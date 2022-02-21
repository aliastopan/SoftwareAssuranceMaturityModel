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
        }

        public Result GetSession(int sessionId)
        {
            var batch = _applicationDbContext.Batches
                .FirstOrDefault(search => search.Session.Id == sessionId);

            if(batch is null)
                return Result.Fail("SESSION NOT FOUND");
            else
                return Result.Ok();
        }

        public void BeganRecap(int sessionId)
        {
            Batches = _applicationDbContext.Batches
                .Where(x => x.Session.Id == sessionId)
                .ToList();

            System.Console.WriteLine($"Batches: {Batches.Count}");

            for (int i = 0; i < 39; i++)
                Averages.Add(new Average());

            TotalRecap();
            _maxQ = Batches[0].Responds.Count;
        }

        private int _maxQ;
        public int MaxQ => _maxQ;
        private IApplicationDbContext _applicationDbContext;
        public List<Batch> Batches = new();
        public List<Average> Averages = new();

        public void TotalRecap()
        {
            for (int i = 0; i < Averages.Count; i++)
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

                Averages[i].Value = avg;
                Averages[i].QDomain = Batches[0].Responds[i].QDomain;

            }
        }

        public List<List<Average>> GetAveragesPerDomain()
        {
            var d1 = Averages.Where(x => x.QDomain == 1).ToList();
            var d2 = Averages.Where(x => x.QDomain == 2).ToList();
            var d3 = Averages.Where(x => x.QDomain == 3).ToList();
            var d4 = Averages.Where(x => x.QDomain == 4).ToList();
            var d5 = Averages.Where(x => x.QDomain == 5).ToList();
            var d6 = Averages.Where(x => x.QDomain == 6).ToList();

            List<List<Average>> averages = new();

            averages.Add(d1);
            averages.Add(d2);
            averages.Add(d3);
            averages.Add(d4);
            averages.Add(d5);
            averages.Add(d6);

            return averages;
        }
        public float AvgPerDomain(int qDomain)
        {
            var avgPerDomain = Averages
                    .Where(v => v.QDomain == qDomain)
                    .ToList();

            float avg = 0f;
            for (int i = 0; i < avgPerDomain.Count; i++)
            {
                avg += avgPerDomain[i].Value;
            }

            return (float) avg/avgPerDomain.Count;
        }
        public float AvgPerQuestionnaire(int qNumber)
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
            return Averages
                .Where(v => v.QDomain == qDomain)
                .ToList().Count;
        }

        public void RespondsPerBatch(int sessionId)
        {
            var singleSession = _applicationDbContext.Batches.ToList();
        }
    }
}