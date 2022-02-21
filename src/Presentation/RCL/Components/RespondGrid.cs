using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Components
{
    public partial class RespondGrid
    {
        [Inject] private RecapManager _recapManager { get; init; } = default!;
    }
}