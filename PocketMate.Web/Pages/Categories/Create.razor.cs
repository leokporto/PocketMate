using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Requests.Categories;

namespace PocketMate.Web.Pages.Categories
{
    public partial class CreateCategoryPage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;

        public CreateCategoryRequest InputModel { get; set; } = new();
        #endregion

        #region services
        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            { 
                var response = await Handler.CreateAsync(InputModel);

                if (response.IsSuccess)
                {
                    Snackbar.Add("Category created successfully", Severity.Success);
                    NavigationManager.NavigateTo("/categories");
                }
                else
                {
                    Snackbar.Add(response.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
