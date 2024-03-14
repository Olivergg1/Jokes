using JokesApp.Handlers;

namespace JokesApp.Services;

public class ApiService : HttpClient
{
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly IConfiguration _configuration;

    private string _baseAddress => _configuration["ApiService:BaseAdress"] ?? "https://localhost:7169/api/";

    public ApiService(JwtTokenHandler jwtTokenHandler, IConfiguration configuration) : base(jwtTokenHandler)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _configuration = configuration;

        BaseAddress = new Uri(_baseAddress);
    }
}
