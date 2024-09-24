using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;
using PocketMate.Core.Requests.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMate.Web.Pages.Transactions
{
    public partial class EditTransactionPage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;

        public UpdateTransactionRequest InputModel { get; set; } = new();

        public List<Category> Categories { get; set; } = [];

        [Parameter]
        public string TransactionId { get; set; } = "";
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

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            await GetTransactionByIdAsync();
            await GetCategoriesAsync();

            IsBusy = false;
        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await TransactionHandler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Transaction updated successfully", Severity.Success);
                    NavigationManager.NavigateTo("/transactions/history");
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
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

        private async Task GetTransactionByIdAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetTransactionByIdRequest { Id = long.Parse(TransactionId) };
                var result = await TransactionHandler.GetByIdAsync(request);
                if (result.IsSuccess && result.Data != null)
                {
                    InputModel = new UpdateTransactionRequest
                    {
                        CategoryId = result.Data.CategoryId,
                        PaidOrReceivedAt = result.Data.AccountedAt,
                        Title = result.Data.Title,
                        Type = result.Data.Type,
                        Amount = result.Data.Amount,
                        Id = result.Data.Id,
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

        private async Task GetCategoriesAsync()
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
    }
}
