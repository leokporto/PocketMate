using Microsoft.AspNetCore.Mvc;
using PocketMate.Api.Common.Api;
using PocketMate.Core;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;
using PocketMate.Core.Responses;
using System.Security.Claims;

namespace PocketMate.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All")
                .WithSummary("Recupera todas as categorias")
                .WithDescription("Recupera todas as categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            [FromQuery] int pageNumber = Configuration.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = Configuration.DEFAULT_PAGE_SIZE)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
