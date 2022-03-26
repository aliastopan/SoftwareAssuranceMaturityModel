using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Actions.Session;
using SoftwareAssuranceMaturityModel.Application.Common.Models.Session;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Components
{
    public partial class SessionForm
    {
        [Inject] private CreateSession _createSession { get; init; } = default!;
        [Inject] private NavigationManager _navManager { get; init; } = default!;
        private NewSessionDto _newSession { get; set; } = new();

        private async Task SubmitAsync()
        {
            await _createSession.Add(_newSession);
            _navManager.NavigateTo("/administrator", forceLoad: true);
        }

    }
}