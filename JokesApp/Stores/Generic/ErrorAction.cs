using System.Net;

namespace JokesApp.Stores.Generic;

public abstract record ErrorAction
{
    public string Reason { get; init; } = "An error occurred😔";

    public HttpStatusCode? StatusCode { get; init; }
}