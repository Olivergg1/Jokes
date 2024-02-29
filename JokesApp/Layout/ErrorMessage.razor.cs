using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace JokesApp.Layout;

public partial class ErrorMessage
{
    [Parameter]
    public string Message { get; set; } = "An error occurred";

    [Parameter]
    public Action? OnRetry { get; set; }

    private void handleRetry()
    {
        if (OnRetry != null)
        {
            OnRetry?.Invoke();
        }
    }
}