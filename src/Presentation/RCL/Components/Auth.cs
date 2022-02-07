using Microsoft.AspNetCore.Components;
using SoftwareAssuranceMaturityModel.Application.Common.Sercurity;

namespace SoftwareAssuranceMaturityModel.Presentation.RCL.Components
{
    public partial class Auth
    {
        [Inject] protected Authentication auth { get; set; } = default!;
        [Inject] protected NavigationManager navManager { get; set;} = default!;
        private bool _isAuthenticating;
        private bool _isNullForm
        {
            get { return string.IsNullOrWhiteSpace(auth.Username) || string.IsNullOrWhiteSpace(auth.Password); }
        }

        private async Task SignInAsync()
        {
            if(_isNullForm)
                return;

            _isAuthenticating = true;

            var authentication = await Task.Run(() => auth.SignInAsync());
            if(authentication.Success)
            {
                navManager.NavigateTo("/");
            }

            _isAuthenticating = false;
        }

    }
}