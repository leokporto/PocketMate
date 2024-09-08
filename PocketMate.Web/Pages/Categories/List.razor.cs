using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;

namespace PocketMate.Web.Pages.Categories
{
    public partial class ListCategoriesPage : ComponentBase
    {

        #region properties
        public bool IsBusy { get; set; } = false;

        public List<Category> CategoriesList { get; set; } = [];

        public string SearchTerm { get; set; } = "";
        #endregion

        #region services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService DialogSvc { get; set; } = null!;

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

        #region methods
        public Func<Category, bool> Filter => category =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            if(category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if(category.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if(category is not null && category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public async Task OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogSvc.ShowMessageBox("Warning",
                                $"The {title} category will be permanently removed. Do you want to proceed?",
                                yesText: "Remove", cancelText: "Cancel");
            
            if(result == true)
                await OnDeleteAsync(id);

            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id)
        {
            try
            { 
                await Handler.DeleteAsync(new DeleteCategoryRequest { Id = id });
                CategoriesList.RemoveAll(x => x.Id == id);
                Snackbar.Add("Category removed successfully", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        #endregion
    }
}
