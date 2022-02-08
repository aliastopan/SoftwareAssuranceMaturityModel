using System;
using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Pages
{
    public partial class SurveyPage
    {
        [Inject] SurveyManager SurveyManager { get; set; } = default!;

        private int _currentPage => SurveyManager.CurrentIndex + 1;
        private int _maxPage => SurveyManager.Questionnaires.Count;
        private bool _isDisabled = true;

        protected override void OnInitialized()
        {
            Console.WriteLine($"Count: {SurveyManager.Responds.Count}");
        }

        protected void Next()
        {
            if(SurveyManager.CurrentRespond.Value == 0)
                return;

            SurveyManager.Next();
            StateHasChanged();
        }

        protected void Prev()
        {
            SurveyManager.Prev();
            StateHasChanged();
        }

        protected void RespondsCheck()
        {
            System.Console.WriteLine("Respond Check");
            foreach (var respond in SurveyManager.Responds)
            {
                if(respond.Value == 0)
                    return;
            }

            _isDisabled = false;
        }
    }
}