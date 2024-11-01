using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Requests;
using ActionCommandGame.Services.Model.Results;
using ActionCommandGame.Services.Model.Core;
using Microsoft.Extensions.Logging;

namespace ActionCommandGame.Sdk
{
    public class IdentitySdk
    {
        private readonly ILogger<IdentitySdk> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public IdentitySdk(ILogger<IdentitySdk> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<AuthenticationResult> SignIn(UserSignInRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGameApi");

            var route = "api/identity/sign-in"; 
            var fullUrl = $"{httpClient.BaseAddress}{route}";
            _logger.LogInformation($"Requesting URL: {fullUrl}");

            var response = await httpClient.PostAsJsonAsync(route, request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Request to {fullUrl} failed with status code {response.StatusCode}");
                response.EnsureSuccessStatusCode(); 
            }

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

            var route = "api/identity/register"; // Ensure this matches the API route exactly
            var fullUrl = $"{httpClient.BaseAddress}{route}";
            _logger.LogInformation($"Requesting URL: {fullUrl}");

            var response = await httpClient.PostAsJsonAsync(route, request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Request to {fullUrl} failed with status code {response.StatusCode}");
                response.EnsureSuccessStatusCode(); // This will throw an exception
            }

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }
    }
}