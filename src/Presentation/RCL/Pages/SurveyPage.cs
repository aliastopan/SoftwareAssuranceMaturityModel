using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class SurveyPage
    {
        [Inject] public QuestionnaireManager QManager { get; set; } = default!;
        private int[] _responds;
        private int _currentChoiceValue;

        public SurveyPage()
        {
            _responds = new int[QManager.Questionnaires.Count];
        }

        private void Check()
        {
            System.Console.WriteLine($"Value: {_currentChoiceValue}");
        }

        private void RadioSelection(ChangeEventArgs args)
        {
            if(args.Value is not null)
            {
                string argsValue = args.Value.ToString()!;
                _currentChoiceValue = int.Parse(argsValue);

                // var index = QManager.CurrentQuestionnaire.

                System.Console.WriteLine($"Value: {_currentChoiceValue}");

            }
        }
    }
}