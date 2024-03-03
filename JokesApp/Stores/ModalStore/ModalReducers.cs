using Fluxor;

namespace JokesApp.Stores.ModalStore;

public class ModalReducers
{
    [ReducerMethod]
    public static ModalState ReduceShowModalAction(ModalState state, ShowModalAction action)
    {
        return state with
        {
            Component = action.Component,
            IsVisible = true
        };
    }

    [ReducerMethod(typeof(CloseModalAction))]
    public static ModalState ReduceCloseModalAction(ModalState state)
    {
        return state with
        {
            IsVisible = false
        };
    }
}
