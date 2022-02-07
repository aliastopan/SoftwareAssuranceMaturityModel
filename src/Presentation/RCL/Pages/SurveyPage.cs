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

        protected override void OnInitialized()
        {
            Console.WriteLine($"Count: {SurveyManager.Responds.Count}");
        }

        protected void Next()
        {
            SurveyManager.Next();
            StateHasChanged();
        }

        protected void Prev()
        {
            SurveyManager.Prev();
            StateHasChanged();
        }


        // [Inject] public QuestionnaireManager QManager { get; set; } = default!;
        // private int[] _responds = new int[0];
        // private int _currentChoiceValue;

        // private int _currentPage => QManager.CurrentIndex + 1;
        // private int _maxPage => QManager.Questionnaires.Count;

        // protected override void OnInitialized()
        // {
        //     _responds = new int[QManager.Questionnaires.Count];
        // }

        // private void RadioSelection(ChangeEventArgs args)
        // {
        //     if(args.Value is not null)
        //     {
        //         string argsValue = args.Value.ToString()!;
        //         _currentChoiceValue = int.Parse(argsValue);
        //         System.Console.WriteLine($"Value: {_currentChoiceValue}");
        //     }
        // }

        // private void Next()
        // {
        //     QManager.Next();
        //     StateHasChanged();
        // }

        // private void Prev()
        // {
        //     QManager.Prev();
        //     StateHasChanged();
        // }

    }
}