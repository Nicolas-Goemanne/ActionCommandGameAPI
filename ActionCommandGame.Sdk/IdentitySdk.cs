using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;
using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Sdk
{
    public class IdentitySdk(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<AuthenticationResult> SignIn(UserSignInRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGameApi");

            var route = "Identity/sign-in";
            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }

        public async Task<AuthenticationResult> Register(UserRegisterRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGameApi");

            var route = "Identity/register";
            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }
    }
}