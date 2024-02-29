namespace JokesApp.Stores.Generic;

public record ErrorableState
{
    public bool HasErrored = false;

    public string ErrorMessage = string.Empty;
}
