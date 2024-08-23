using PocketMate.Core.Handlers;
using PocketMate.Core.Requests.Account;
using PocketMate.Core.Responses;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace PocketMate.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _client = 
            httpClientFactory.CreateClient(Configuration.HTTP_CLIENT_NAME);

        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
            return result.IsSuccessStatusCode
                ? new Response<string>("Login realizado com sucesso!", (int)HttpStatusCode.OK, "Login realizado com sucesso!")
                : new Response<string>(null, (int)HttpStatusCode.BadRequest, "Não foi possível realizar o login");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/register", request);
            return result.IsSuccessStatusCode
                ? new Response<string>("Cadastro realizado com sucesso!", (int)HttpStatusCode.Created, "Cadastro realizado com sucesso!")
                : new Response<string>(null, (int)HttpStatusCode.BadRequest, "Não foi possível realizar o seu cadastro");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
        }
    }
}
