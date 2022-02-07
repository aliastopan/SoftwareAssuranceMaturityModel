using System.Collections.Generic;
using System.Text.Json;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class SurveyManager
    {
        public SurveyManager()
        {
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

        }
        public List<Questionnaire> Questionnaires { get; set; } = new();
        public readonly List<Respond> Responds;
        public int CurrentIndex { get; private set; }
        public Questionnaire CurrentQuestionnaire => Questionnaires[CurrentIndex];
        public Respond CurrentRespond => Responds[CurrentIndex];


        public void Next()
        {
            if(CurrentIndex < Questionnaires.Count)
                CurrentIndex++;
        }

        public void Prev()
        {
            if(CurrentIndex > 0)
                CurrentIndex--;
        }

    }
}