using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class SurveyManager
    {
        private IApplicationDbContext _applicationDbContext;

        public SurveyManager(IApplicationDbContext  applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            string source = Marshal.GetDataStorage("survey-3.json", @"..\..\..\");
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