using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Transactions;
using PocketMate.Core.Responses;

namespace PocketMate.Web.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        public Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
