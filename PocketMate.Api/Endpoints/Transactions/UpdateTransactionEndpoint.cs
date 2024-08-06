using PocketMate.Api.Common.Api;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Transactions;
using PocketMate.Core.Responses;
using System.Security.Claims;

namespace PocketMate.Api.Endpoints.Transactions
{
	public class UpdateTransactionEndpoint : IEndpoint
	{
		public static void Map(IEndpointRouteBuilder app)
			=> app.MapPut("/{id}", HandleAsync)
				.WithName("Transactions: Update")
				.WithSummary("Atualiza uma transação")
				.WithDescription("Atualiza uma transação")
				.WithOrder(2)
				.Produces<Response<Transaction?>>();

		private static async Task<IResult> HandleAsync(
			ClaimsPrincipal user,
			ITransactionHandler handler,
			UpdateTransactionRequest request,
			long id)
		{
			request.UserId = user.Identity?.Name ?? string.Empty;
			request.Id = id;

			var result = await handler.UpdateAsync(request);
			return result.IsSuccess
				? TypedResults.Ok(result)
				: TypedResults.BadRequest(result);
		}
	}
}
