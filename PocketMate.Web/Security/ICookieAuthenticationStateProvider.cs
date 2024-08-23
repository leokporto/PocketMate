using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace PocketMate.Web.Security
{
    public interface ICookieAuthenticationStateProvider
    {
        Task<bool> CheckAuthenticatedAsync();
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void NotifyAuthenticationStateChanged();
    }
}
