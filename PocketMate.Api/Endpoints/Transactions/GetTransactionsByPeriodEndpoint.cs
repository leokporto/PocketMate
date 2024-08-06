using Microsoft.AspNetCore.Mvc;
using PocketMate.Api.Common.Api;
using PocketMate.Core;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Transactions;
using PocketMate.Core.Responses;
using System.Security.Claims;

namespace PocketMate.Api.Endpoints.Transactions
{
	public class GetTransactionsByPeriodEndpoint : IEndpoint
	{
		public static void Map(IEndpointRouteBuilder app)
			=> app.MapGet("/", HandleAsync)
				.WithName("Transactions: Get All")
				.WithSummary("Recupera todas as transações")
				.WithDescription("Recupera todas as transações")
				.WithOrder(5)
				.Produces<PagedResponse<List<Transaction>?>>();

		private static async Task<IResult> HandleAsync(
			ClaimsPrincipal user,
			ITransactionHandler handler,
			[FromQuery] DateTime? startDate = null,
			[FromQuery] DateTime? endDate = null,
			[FromQuery] int pageNumber = Configuration.DEFAULT_PAGE_NUMBER,
			[FromQuery] int pageSize = Configuration.DEFAULT_PAGE_SIZE)
		{
			var request = new GetTransactionsByPeriodRequest
			{
				UserId = user.Identity?.Name ?? string.Empty,
				PageNumber = pageNumber,
				PageSize = pageSize,
				StartDate = startDate,
				EndDate = endDate
			};

			var result = await handler.GetByPeriodAsync(request);
			return result.IsSuccess
				? TypedResults.Ok(result)
				: TypedResults.BadRequest(result);
		}
	}
}
