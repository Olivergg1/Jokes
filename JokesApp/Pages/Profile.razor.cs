using Microsoft.AspNetCore.Components;

namespace JokesApp.Pages;

public partial class Profile
{
    [Parameter]
    public int? Id { get; set; }
}
