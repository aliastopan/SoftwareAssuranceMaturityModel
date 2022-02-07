using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Models.User;

namespace SoftwareAssuranceMaturityModel.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<Result> SignInAsync(string username, string password, bool persistentSignIn = false, string? authType = null);
        Task SignOutAsync();
    }
}