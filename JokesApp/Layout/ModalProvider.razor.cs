using Fluxor;
using JokesApp.Stores.ModalStore;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Layout;

public partial class ModalProvider
{
    [Inject]
    public IState<ModalState>? ModalState { get; set; }

    public bool IsModalVisible => ModalState?.Value.IsVisible ?? false;

    public string ModalProviderStyle => IsModalVisible ? "active" : string.Empty;

    public Type? ModalType => ModalState?.Value.Component;

    public RenderFragment CreateComponent() => builder =>
    {
        if (ModalType != null)
        {
            builder.OpenComponent(0, ModalType);
            builder.CloseComponent();
        }
    };
}
