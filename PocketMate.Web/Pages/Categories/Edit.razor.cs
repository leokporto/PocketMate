using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Requests.Categories;

namespace PocketMate.Web.Pages.Categories
{
    public partial class EditCategoryPage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;

        public UpdateCategoryRequest InputModel { get; set; } = new();

        [Parameter]
        public string Id { get; set; } = "";
        #endregion

        #region services

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region events
        protected async override Task OnInitializedAsync()
        {
            GetCategoryByIdRequest? request = null;

            try
            {
                request = new GetCategoryByIdRequest
                {
                    Id = long.Parse(Id)
                };
            }
            catch (Exception)
            {

                Snackbar.Add("Invalid parameter", Severity.Error);
            }

            try
            {
                if (request == null)
                    return;

                IsBusy = true;

                var response = await Handler.GetByIdAsync(request);

                if (response.IsSuccess && response.Data != null)
                {
                    InputModel = new UpdateCategoryRequest
                    {
                        Id = response.Data.Id,
                        Title = response.Data.Title,
                        Description = response.Data.Description
                    };
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

        #region methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Category updated successfully", Severity.Success);
                    NavigationManager.NavigateTo("/categories");
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
