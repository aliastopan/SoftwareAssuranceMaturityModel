using System.Collections.Generic;
using System.Text.Json;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class QuestionnaireManager
    {
        public List<Questionnaire> Questionnaires { get; set; }

        public QuestionnaireManager()
        {
            string source = Marshal.GetDataStorage("survey.json", @"..\..\..\");

            using (StreamReader reader = new StreamReader(source))
            {
                string json = reader.ReadToEnd();
                var result = JsonSerializer.Deserialize<List<Questionnaire>>(json);
                Questionnaires = result is not null ? result : new();
            }
        }

        public Result<List<Questionnaire>> TryGetQuestionnaires()
        {
            string source = Marshal.GetDataStorage("survey.json", @"..\..\..\");

            using (StreamReader reader = new StreamReader(source))
            {
                string json = reader.ReadToEnd();
                var result = JsonSerializer.Deserialize<List<Questionnaire>>(json);

                if(result is null)
                    return Result.Fail<List<Questionnaire>>("SOURCE IS EMPTY");
                else
                    return Result.Ok<List<Questionnaire>>(result);
            }

        }
    }
}