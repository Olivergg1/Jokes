using Fluxor;
using JokesApp.Layout;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Stores.ModalStore;

[FeatureState]
public record ModalState
{
    public bool IsVisible { get; set; } = false;

    public Type? Component { get; set; }
}