using Microsoft.AspNetCore.Identity;
using PocketMate.Api.Common.Api;
using PocketMate.Api.Models;

namespace PocketMate.Api.Endpoints.Identity
{
	public class LogoutEndpoint : IEndpoint
	{
		public static void Map(IEndpointRouteBuilder app)
			=> app
				.MapPost("/logout", HandleAsync)
				.RequireAuthorization();

		private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
		{
			await signInManager.SignOutAsync();
			return Results.Ok();
		}
	}
}
