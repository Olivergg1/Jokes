using Fluxor;
using JokesApp.Stores.ModalStore;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Layout;

public interface IModal { }

public partial class Modal
{
    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Title { get; set; }

    public void HandleClose()
    {
        Dispatcher?.Dispatch(new CloseModalAction());
    }
}
