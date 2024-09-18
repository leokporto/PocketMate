using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;
using PocketMate.Core.Requests.Transactions;

namespace PocketMate.Web.Pages.Transactions
{
    public partial class CreateTransactionPage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;

        public CreateTransactionRequest InputModel { get; set; } = new();

        public List<Category> Categories { get; set; } = [];
        #endregion

        #region services
        [Inject]
        public ITransactionHandler TransactionHandler { get; set; } = null!;

        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region events

        protected async override Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await CategoryHandler.GetAllAsync(request);

                if (result.IsSuccess)
                {
                    Categories = result.Data ?? [];
                    InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
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
                var response = await TransactionHandler.CreateAsync(InputModel);

                if (response.IsSuccess)
                {
                    Snackbar.Add("Transaction created successfully", Severity.Success);
                    NavigationManager.NavigateTo("/transactions/history");
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
