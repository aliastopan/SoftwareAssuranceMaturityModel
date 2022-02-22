using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class QuestionnaireManager
    {
        private IHostingEnvironment _env;
        public List<Questionnaire> Questionnaires { get; } = new();
        public Questionnaire EditableQ => Questionnaires[CurrentIndex];
        public int CurrentIndex { get; set; }

        public QuestionnaireManager(IHostingEnvironment env)
        {
            _env = env;

            string wwwroot = $"{env.WebRootPath}\\.datastorage\\survey.json";
            using (StreamReader reader = new StreamReader(wwwroot))
            {
                string json = reader.ReadToEnd();
                var result = JsonSerializer.Deserialize<List<Questionnaire>>(json);

                if(result is not null)
                {
                    Questionnaires = result;
                    CurrentIndex = 0;
                }
            }

        }
    }
}