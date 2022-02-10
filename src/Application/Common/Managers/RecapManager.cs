using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class RecapManager
    {
        public RecapManager(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            Batches = _applicationDbContext.Batches
                .Where(x => x.Session.Id == 1)
                .ToList();

            System.Console.WriteLine($"Batches: {Batches.Count}");

            for (int i = 0; i < 39; i++)
                AvgPerQ.Add(new Average());

            TotalRecap();
            _maxQ = Batches[0].Responds.Count;
        }

        private int _maxQ;
        public int MaxQ => _maxQ;
        private IApplicationDbContext _applicationDbContext;
        public List<Batch> Batches = new();
        public List<Average> AvgPerQ = new();

        public void TotalRecap()
        {
            for (int i = 0; i < AvgPerQ.Count; i++)
            {
                var responds = _applicationDbContext.Responds
                    .Where(n => n.QNumber == i)
                    .ToList();

                float avg = 0f;
                for (int j = 0; j < responds.Count; j++)
                {
                    avg += (float) responds[j].Value;
                }
                avg = (float) avg/responds.Count;

                AvgPerQ[i].Value = avg;
                AvgPerQ[i].QDomain = Batches[0].Responds[i].QDomain;

            }
        }

        public float PerDomainAvg(int qDomain)
        {
            var avgPerDomain = AvgPerQ
                    .Where(v => v.QDomain == qDomain)
                    .ToList();

            float avg = 0f;
            for (int i = 0; i < avgPerDomain.Count; i++)
            {
                avg += avgPerDomain[i].Value;
            }

            return (float) avg/avgPerDomain.Count;
        }

        public float PerQuestionnaireAvg(int qNumber)
        {
            var responds = _applicationDbContext.Responds
                .Where(n => n.QNumber == qNumber)
                .ToList();

            float avg = 0f;
            for (int i = 0; i < responds.Count; i++)
            {
                avg += (float) responds[i].Value;
            }
            return (float) avg/responds.Count;
        }

        public int QPerDomainCount(int qDomain)
        {
            return AvgPerQ
                .Where(v => v.QDomain == qDomain)
                .ToList().Count;
        }
    }
}