using Fluxor;
using JokesApp.Models;
using JokesApp.Responses;
using JokesApp.Services;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace JokesApp.Providers;

public interface IJwtTokenProvider
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task RegisterAsync(User user);
}

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private bool _isAuthenticated = false;

    private readonly ClaimsPrincipal Unauthenticated = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly ApiService _apiService;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IDispatcher _dispatcher;

    public JwtAuthenticationStateProvider(ApiService apiService, JwtTokenService tokenService, IDispatcher dispatcher)
    {
        _apiService = apiService;
        _jwtTokenService = tokenService;
        _dispatcher = dispatcher;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var result = await _apiService.PostAsJsonAsync("Authentication/login", new { Username = username, Password = password });
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadFromJsonAsync<JwtTokenResponse>();
            
            await _jwtTokenService.SaveTokenAsync(response.Token);
            
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        const string Empty = "{}";
        var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");
        var response = await _apiService.PostAsync("Authentication/logout", emptyContent);
        
        if (response.IsSuccessStatusCode)
        {
            await _jwtTokenService.ClearTokenAsync();

            _dispatcher.Dispatch(new UserLogoutSucceededAction());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public async Task RegisterAsync(User user)
    {
        try
        {
            // TODO: Implement user register logic
            var result = await _apiService.PostAsJsonAsync("register", user);
            result.EnsureSuccessStatusCode();
        }
        catch { }
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;

        var user = Unauthenticated;

        try
        {
            var response = await _apiService.GetAsync("Authentication/");

            response.EnsureSuccessStatusCode();

            // user is authenticated,so let's build their authenticated identity
            var userResponse = await response.Content.ReadFromJsonAsync<User>();

            if (userResponse != null)
            {
                // in our system name and email are the same
                var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userResponse.Username),
                        new(ClaimTypes.Email, userResponse.Email)
                    };

                // add any additional claims
                claims.AddRange(
                    userResponse.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email)
                        .Select(c => new Claim(c.Key, c.Value)));

                // set the principal
                var id = new ClaimsIdentity(claims, nameof(JwtAuthenticationStateProvider));
                user = new ClaimsPrincipal(id);
                _isAuthenticated = true;

                _dispatcher.Dispatch(new UserAuthenticateSucceededAction(userResponse));
            }
        }
        catch { }

        return new AuthenticationState(user);
    }

    public async Task<bool> CheckAuthenticationAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }
}