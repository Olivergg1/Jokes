using Fluxor;

namespace JokesApp.Services;

public abstract class BaseService
{
    public ApiService ApiService { get; init; }

    public IDispatcher Dispatcher { get; init; }

    public BaseService(ApiService httpClient, IDispatcher dispatcher)
    {
        ApiService = httpClient;
        Dispatcher = dispatcher;
    }
}
