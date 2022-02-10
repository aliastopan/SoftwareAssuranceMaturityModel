using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class RecapPage
    {
        [Inject] private RecapManager _recapManager { get; set; } = default!;

        int _maxQ => _recapManager.MaxQ;
        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                System.Console.WriteLine($"Total Q: {_maxQ}");

                int q1 = _recapManager.QPerDomainCount(1);
                int q2 = _recapManager.QPerDomainCount(2);
                int q3 = _recapManager.QPerDomainCount(3);
                int q4 = _recapManager.QPerDomainCount(4);
                int q5 = _recapManager.QPerDomainCount(5);
                int q6 = _recapManager.QPerDomainCount(6);

                System.Console.WriteLine($"Q Count: {q1}");
                System.Console.WriteLine($"Q Count: {q2}");
                System.Console.WriteLine($"Q Count: {q3}");
                System.Console.WriteLine($"Q Count: {q4}");
                System.Console.WriteLine($"Q Count: {q5}");
                System.Console.WriteLine($"Q Count: {q6}");


                // System.Console.WriteLine($"Q #{1} Avg: {_recapManager.Av}");

        
                // _recapManager.TotalRecap();
                // var x = _recapManager.PerDomainAvg(1);
                // System.Console.WriteLine($"Avg Per Domain: {x}");

                // System.Console.WriteLine($"Recap QDomain: {_recapManager.AvgPerQ[2].QDomain}");
                // System.Console.WriteLine($"Recap Value: {_recapManager.AvgPerQ[2].Value}");

            }
        }
    }
}