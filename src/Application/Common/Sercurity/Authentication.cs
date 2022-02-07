using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;

namespace SoftwareAssuranceMaturityModel.Application.Common.Sercurity
{
    public class Authentication
    {
        public Authentication(IAuthService authService)
        {
            _authService = authService;
        }

        private readonly IAuthService _authService;

        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool PersistentSignIn { get; set; }

        public async Task<Result> SignInAsync()
        {
            string? authType = null;
            Result authentication = await _authService.SignInAsync(Username!, Password!, PersistentSignIn, authType);

            if(authentication.Success)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail(authentication.Error!);
            }
        }
    }
}