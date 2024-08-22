using PocketMate.Core.Requests.Account;
using PocketMate.Core.Responses;

namespace PocketMate.Core.Handlers
{
	public interface IAccountHandler
	{
		Task<Response<string>> LoginAsync(LoginRequest request);
		Task<Response<string>> RegisterAsync(RegisterRequest request);
		Task LogoutAsync();
	}
}
