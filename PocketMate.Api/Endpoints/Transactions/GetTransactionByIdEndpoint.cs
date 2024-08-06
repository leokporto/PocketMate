using PocketMate.Api.Common.Api;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Transactions;
using PocketMate.Core.Responses;
using System.Security.Claims;

namespace PocketMate.Api.Endpoints.Transactions
{
	public class GetTransactionByIdEndpoint : IEndpoint
	{
		public static void Map(IEndpointRouteBuilder app)
			=> app.MapGet("/{id}", HandleAsync)
				.WithName("Transactions: Get By Id")
				.WithSummary("Recupera uma transação")
				.WithDescription("Recupera uma transação")
				.WithOrder(4)
				.Produces<Response<Transaction?>>();

		private static async Task<IResult> HandleAsync(
			ClaimsPrincipal user,
			ITransactionHandler handler,
			long id)
		{
			var request = new GetTransactionByIdRequest
			{
				UserId = user.Identity?.Name ?? string.Empty,
				Id = id
			};

			var result = await handler.GetByIdAsync(request);
			return result.IsSuccess
				? TypedResults.Ok(result)
				: TypedResults.BadRequest(result);
		}
	}
}
