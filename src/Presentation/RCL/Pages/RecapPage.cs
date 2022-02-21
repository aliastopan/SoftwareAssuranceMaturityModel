using System;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class RecapPage
    {
        [Inject] private RecapManager _recapManager { get; set; } = default!;
        [Parameter] public int SessionId { get; set; }

        int _maxQ => _recapManager.MaxQ;
        List<List<float>> _recapableResults = new();
        List<float> _result = new();
        // i = 1 => d1
        // i = 2 => d2
        // i = 3 => d3
        // i = 4 => d4
        // i = 5 => d5
        // i = 6 => d6

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

                _result.Add(_recapableResults[i][domainAvg]);
            }

            dataLiterial = CreateLiteralFromData();

        }

        protected override void OnInitialized()
        {
            _recapManager.BeganRecap(SessionId);

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
                    float avg = (float) Math.Round(respondsAvg[j].Value, 2);
                    results.Add(avg);
                    // System.Console.WriteLine($"Avg #{j+1}: {avg} ");
                }
                var qAvg = (float) Math.Round(_recapManager.AvgPerDomain(i+1), 2);
                results.Add(qAvg);
                // System.Console.WriteLine($"DOMAIN AVG {qAvg}\n");

                _recapableResults.Add(results);
            }

            RecapCheck();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {

            }

        }
    }
}