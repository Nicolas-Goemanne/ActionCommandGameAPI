using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class GameSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GameSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<GameResult>> PerformAction(int playerId)
        {
            var httpClient = CreateHttpClient();
            var route = $"/api/game/{playerId}/actions";

            var response = await httpClient.PostAsync(route, null);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<GameResult>>();
            if (result is null)
            {
                return new ServiceResult<GameResult>();
            }

            return result;
        }

        public async Task<ServiceResult<BuyResult>> Buy(int playerId, int itemId)
        {
            var httpClient = CreateHttpClient();
            var route = $"/api/game/{playerId}/buy/{itemId}";

            var response = await httpClient.PostAsync(route, null);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<BuyResult>>();
            if (result is null)
            {
                return new ServiceResult<BuyResult>();
            }

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient("ActionCommandGameApi");
        }
    }
}

