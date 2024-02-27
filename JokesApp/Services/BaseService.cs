using Fluxor;

namespace JokesApp.Services;

public abstract class BaseService
{
    public HttpClient HttpClient { get; init; }

    public IDispatcher Dispatcher { get; init; }

    public BaseService(HttpClient httpClient, IDispatcher dispatcher)
    {
        HttpClient = httpClient;
        Dispatcher = dispatcher;
    }
}
