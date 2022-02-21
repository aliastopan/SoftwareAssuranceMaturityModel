using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;
using SoftwareAssuranceMaturityModel.Application.Common.Models.Session;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class AdministratorPage
    {
        [Inject] public SessionManager SessionManager { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        private void DeleteSession(int sessionId)
        {
            SessionManager.DeleteSession(sessionId);
            StateHasChanged();
            NavigationManager.NavigateTo("/administrator");
        }
    }
}