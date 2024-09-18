using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketMate.Core.Common.Extensions;
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
    public class ListTransactionsPage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;

        public List<Transaction> Transactions { get; set; } = [];

        public string SearchTerm { get; set; } = "";

        public int CurrentYear { get; set; } = DateTime.Now.Year;

        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public int[] Years { get; set; } =
        { 
            DateTime.Now.Year,
            DateTime.Now.AddYears(-1).Year,
            DateTime.Now.AddYears(-2).Year,
            DateTime.Now.AddYears(-3).Year,
            DateTime.Now.AddYears(-4).Year
        };

        #endregion

        #region services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public ITransactionHandler TransactionHandler { get; set; } = null!;
        #endregion

        #region events
        protected async override Task OnInitializedAsync()
        {
            await GetTransactionsAsync();
        }
        #endregion

        #region methods
        public Func<Transaction, bool> Filter => transaction =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            if (transaction.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (transaction.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private async Task GetTransactionsAsync()
        {
            IsBusy = true;

            try
            {
                DateTime startDate = new DateTime(CurrentYear, CurrentMonth, 1);
                var request = new GetTransactionsByPeriodRequest()
                { 
                    StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                    EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                    PageNumber = 1,
                    PageSize = 1000
                };
                var result = await TransactionHandler.GetByPeriodAsync(request);
                if (result.IsSuccess)
                {
                    Transactions = result.Data ?? [];
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

        public async Task OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogService.ShowMessageBox("Warning",
                                $"The {title} transaction will be permanently removed. Do you want to proceed?",
                                yesText: "Remove", cancelText: "Cancel");

            if (result == true)
                await OnDeleteAsync(id);

            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id)
        {
            IsBusy = true;
            try
            {
                var result = await TransactionHandler.DeleteAsync(new DeleteTransactionRequest { Id = id });

                if (result.IsSuccess)
                {
                    Transactions.RemoveAll(x => x.Id == id);
                    Snackbar.Add("Transaction removed successfully", Severity.Success);
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

        public async Task OnSearchAsync()
        {
            await GetTransactionsAsync();
            StateHasChanged();
        }

        #endregion
    }
}
