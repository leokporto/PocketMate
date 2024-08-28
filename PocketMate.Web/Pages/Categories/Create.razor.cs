using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Requests.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMate.Web.Pages.Categories
{
    public class CreatePage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;

        public CreateCategoryRequest InputModel { get; set; } = new();
        #endregion

        #region services
        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;

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
                var response = await CategoryHandler.CreateAsync(InputModel);

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
