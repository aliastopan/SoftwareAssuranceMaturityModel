using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Mapster;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Models.User;

namespace SoftwareAssuranceMaturityModel.Infrastructure.Services
{
    public class AuthService : AuthenticationStateProvider, IAuthService
    {
        public AuthService(IUserDbContext userDbContext, ICurrentUserService currentUser , BrowserStorageService storageService)
        {
            _userDbContext = userDbContext;
            _currentUser = currentUser;
            _storageService = storageService;
        }

        private readonly IUserDbContext _userDbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly BrowserStorageService _storageService;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();

            try
            {
                var persistentSignIn = await _storageService.LocalStorage!.GetAsync<string>("identity");

                if(persistentSignIn.Success)
                {
                    string jsonIdentity = persistentSignIn.Value!;
                    Identity identity = JsonSerializer.Deserialize<Identity>(jsonIdentity)!;
                    Result persistent = Validate(identity.Username, identity.AuthType);

                    _currentUser.SetCurrentUser(identity.Id);

                    if(persistent.Success)
                    {
                        var claims = CreateClaims(identity);
                        principal = new ClaimsPrincipal(claims);
                    }
                    else
                    {
                        await _storageService.LocalStorage.DeleteAsync("identity");
                    }
                }
            }
            catch { }

            return await Task.FromResult(
                new AuthenticationState(principal)
            );
        }

        public async Task<Result> SignInAsync(string username, string password, bool persistentSignIn = false, string? authType = null)
        {
            Result<Identity> validation = Validate(username, authType);

            if(validation.Failure)
            {
                return Result.Fail<Identity>(validation.Error!);
            }

            Identity identity = validation.Value;
            Result authentication = Authenticate(identity, password);

            if(authentication.Failure)
            {
                return Result.Fail(authentication.Error!);
            }
            else
            {
                ClaimsIdentity claims = CreateClaims(identity);
                ClaimsPrincipal principal = new ClaimsPrincipal(claims);

                if(persistentSignIn)
                {
                    string jsonIdentity = JsonSerializer.Serialize<Identity>(identity);
                    await _storageService.LocalStorage.SetAsync("identity", jsonIdentity);
                }

                AuthenticationState signIn = new AuthenticationState(principal);

                _currentUser.SetCurrentUser(identity.Id);
                NotifyAuthenticationStateChanged(Task.FromResult(signIn));
                return Result.Ok();
            }
        }

        public async Task SignOutAsync()
        {
            await _storageService.LocalStorage.DeleteAsync("identity");

            ClaimsPrincipal principal = new ClaimsPrincipal();
            AuthenticationState signOut = new AuthenticationState(principal);

            NotifyAuthenticationStateChanged(Task.FromResult(signOut));
        }

        private Result<Identity> Validate(string username, string? authType)
        {
            var user = _userDbContext.Users.FirstOrDefault(search => search.Username == username);

            if(user is null)
            {
                return Result.Fail<Identity>(ErrorMessage.USER_NOT_FOUND);
            }
            else
            {
                Identity identity = user.Adapt<Identity>();
                identity.AuthType = authType is null ? "apiauth" : authType;

                return Result.Ok<Identity>(identity);
            }
        }

        private Result Authenticate(Identity identity, string password)
        {
            password = Cryptograph.GetHash(password, salt: identity.Id.ToString());

            if(identity.Password != password)
                return Result.Fail(ErrorMessage.INVALID_PASSWORD);
            else
                return Result.Ok();
        }

        private ClaimsIdentity CreateClaims(Identity identity)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, identity.Username),
                new Claim(ClaimTypes.Role, identity.Role.ToString()),
                new Claim(type: "Flag", identity.Flag.ToString())
            };

            return new ClaimsIdentity(claims, identity.AuthType);
        }
    }
}