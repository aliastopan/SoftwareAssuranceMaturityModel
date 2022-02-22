using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class SurveyManager
    {
        #region RECAP
        public List<List<Respond>> RecapResponds = new();

        private void Recap()
        {
            string source = Marshal.GetDataStorage("responds.json", @"..\..\..\");
            using (StreamReader reader = new StreamReader(source))
            {
                string json = reader.ReadToEnd();
                List<int[]> recap = JsonSerializer.Deserialize<List<int[]>>(json)!;

                int totalRespondent = recap.Count;
                int totalRespond = recap[0].Length;

                for (int i = 0; i < totalRespondent; i++)
                {
                    var perRespond = new List<Respond>();
                    for (int j = 0; j < totalRespond; j++)
                    {
                        int domain = DictionaryDomain.Domains[Questionnaires[j].Domain];

                        perRespond.Add(new Respond{
                            QNumber = j,
                            QDomain = domain,
                            Value = recap[i][j]
                        });
                    }

                    RecapResponds.Add(perRespond);
                }
            }

            int x = RecapResponds.Count;
            int y = RecapResponds[0].Count;

            System.Console.WriteLine($"Recap: {x * y}");
        }

        public async Task SubmitRecapAsync()
        {
            var session = await GetCurrentSession();
            if(session.Failure)
                return;

            int totalRespondent = RecapResponds.Count;

            for (int i = 0; i < totalRespondent; i++)
            {
                Batch batch = new Batch{
                    Session = session.Value,
                    Responds = RecapResponds[i]
                };

                await _applicationDbContext.Batches.AddAsync(batch);
                _applicationDbContext.SaveChanges();
            }

        }

        #endregion



        private IApplicationDbContext _applicationDbContext;
        private IHostingEnvironment _env;

        public SurveyManager(IApplicationDbContext  applicationDbContext, IHostingEnvironment env)
        {
            _applicationDbContext = applicationDbContext;
            _env = env;

            string wwwroot = $"{env.WebRootPath}\\.datastorage\\survey.json";
            // string rootsource = Marshal.GetDataStorage("survey.json", ".");
            // System.Console.WriteLine($"ROOT SOURCE: {wwwroot}");


            string source = Marshal.GetDataStorage("survey.json", @"..\..\..\");
            using (StreamReader reader = new StreamReader(source))
            {
                string json = reader.ReadToEnd();
                var result = JsonSerializer.Deserialize<List<Questionnaire>>(json);

                if(result is not null)
                {
                    Questionnaires = result;
                    CurrentIndex = 0;
                }
            }

            Responds = new();
            for (int i = 0; i < Questionnaires.Count; i++)
                Responds.Add(new());

            // Recap();

        }
        public List<Questionnaire> Questionnaires { get; set; } = new();
        public readonly List<Respond> Responds;
        public int CurrentIndex { get; private set; }
        public Questionnaire CurrentQuestionnaire => Questionnaires[CurrentIndex];
        public Respond CurrentRespond => Responds[CurrentIndex];


        public void Next()
        {
            if(CurrentIndex < (Questionnaires.Count - 1))
                CurrentIndex++;
        }

        public void Prev()
        {
            if(CurrentIndex > 0)
                CurrentIndex--;
        }

        public async Task Submit()
        {
            var session = await GetCurrentSession();
            if(session.Failure)
                return;

            Batch batch = new Batch{
                Session = session.Value,
                Responds = this.Responds
            };

            await _applicationDbContext.Batches.AddAsync(batch);
            _applicationDbContext.SaveChanges();
        }

        private async Task<Result<Session>> GetCurrentSession()
        {
            var result = await _applicationDbContext.Sessions
                .OrderBy(x => x.Id)
                .LastOrDefaultAsync<Session>();

            if(result is null)
                return Result.Fail<Session>(ErrorMessage.NO_SESSION_FOUND);

            if(result.Flag != SessionFlag.OnGoing)
                return Result.Fail<Session>(ErrorMessage.NO_SESSION_FOUND);
            else
                return Result.Ok<Session>(result);
        }

    }
}