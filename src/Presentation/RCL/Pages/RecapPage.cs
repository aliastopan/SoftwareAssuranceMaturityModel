using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class RecapPage
    {
        [Inject] private RecapManager _recapManager { get; set; } = default!;

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                _recapManager.TotalRecap();
                var x = _recapManager.PerDomainAvg(1);
                System.Console.WriteLine($"Avg Per Domain: {x}");

                // System.Console.WriteLine($"Recap QDomain: {_recapManager.AvgPerQ[2].QDomain}");
                // System.Console.WriteLine($"Recap Value: {_recapManager.AvgPerQ[2].Value}");

            }
        }
    }
}