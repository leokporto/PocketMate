using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Requests.Account;
using PocketMate.Web.Security;

namespace PocketMate.Web.Pages.Identity
{
    public partial class LoginPage : ComponentBase
    {
        #region dependencies
        [Inject]
        public ISnackbar SnackBar { get; set; } = null!;

        [Inject]
        public IAccountHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        #region properties

        public bool IsBusy { get; set; } = false;

        public LoginRequest InputModel { get; set; } = new();
        #endregion

        #region events
        protected async override Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                NavigationManager.NavigateTo("/");
            }
        }
        #endregion

        #region methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.LoginAsync(InputModel);

                if (result.IsSuccess)
                {
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    SnackBar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }


        #endregion
    }
}
