using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;

namespace PocketMate.Web.Pages.Categories
{
    public class ListCategoriesPage : ComponentBase
    {

        #region properties
        public bool IsBusy { get; set; } = false;

        public List<Category> CategoriesList { get; set; } = [];
        #endregion

        #region services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        #endregion

        #region events
        protected async override Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    CategoriesList = result.Data ?? [];
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
