using System.Net;

namespace JokesApp.Stores.Generic;

public abstract record ErrorAction
{
    public string? Reason { get; init; }

    public HttpStatusCode? StatusCode { get; init; }
}