using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace SoftwareAssuranceMaturityModel.Infrastructure.Services
{
    public class BrowserStorageService
    {
        public readonly ProtectedLocalStorage LocalStorage;
        public readonly ProtectedSessionStorage SessionStorage;

        public BrowserStorageService(ProtectedLocalStorage localStorage, ProtectedSessionStorage sessionStorage)
        {
            LocalStorage = localStorage;
            SessionStorage = sessionStorage;
        }
    }
}