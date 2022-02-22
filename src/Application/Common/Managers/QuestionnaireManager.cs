using System.Text.Json;
using System.IO;
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

            string wwwroot = $"{_env.WebRootPath}\\.datastorage\\survey.json";
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

        public void Save()
        {
            Questionnaires[CurrentIndex] = EditableQ;
            string wwwroot = $"{_env.WebRootPath}\\.datastorage\\survey.json";
            var jsonFormat = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize<List<Questionnaire>>(Questionnaires, jsonFormat);

            File.WriteAllText(wwwroot, json);
        }

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

    }
}