using System.Security.Authentication.ExtendedProtection;
using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class RecapPage
    {
        [Inject] private RecapManager _recapManager { get; set; } = default!;

        int _maxQ => _recapManager.MaxQ;
        List<List<float>> _recapableResults = new();

        protected void RecapCheck()
        {
            for (int i = 0; i < _recapableResults.Count; i++)
            {
                System.Console.WriteLine($"Domain #{i+1}");
                for (int j = 0; j < _recapableResults[i].Count - 1; j++)
                {
                    System.Console.WriteLine($"Avg #{j+1}: {_recapableResults[i][j]} ");
                }
                int domainAvg = _recapableResults[i].Count - 1;
                System.Console.WriteLine($"DOMAIN AVG {_recapableResults[i][domainAvg]}\n");

            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                System.Console.WriteLine($"Total Q: {_maxQ}");

                var recapable = _recapManager.GetAveragesPerDomain();
                var _q1 = recapable[0];


                for (int i = 0; i < recapable.Count; i++)
                {
                    List<float> results = new();
                    var respondsAvg = recapable[i];
                    // System.Console.WriteLine($"Domain #{i+1}");
                    for (int j = 0; j < respondsAvg.Count; j++)
                    {
                        float avg = respondsAvg[j].Value;
                        results.Add(avg);
                        // System.Console.WriteLine($"Avg #{j+1}: {avg} ");
                    }
                    var qAvg = _recapManager.AvgPerDomain(i+1);
                    results.Add(qAvg);
                    // System.Console.WriteLine($"DOMAIN AVG {qAvg}\n");

                    _recapableResults.Add(results);
                }
            }
        
            RecapCheck();
        }
    }
}