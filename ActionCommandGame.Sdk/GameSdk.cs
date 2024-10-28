using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class GameSdk
    {
        private readonly HttpClient _httpClient;

        public GameSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<GameResult>> PerformActionAsync(int playerId)
        {
            var response = await _httpClient.PostAsync($"/api/game/{playerId}/actions", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResult<GameResult>>();
        }

        public async Task<ServiceResult<BuyResult>> BuyItemAsync(int playerId, int itemId)
        {
            var response = await _httpClient.PostAsync($"/api/game/{playerId}/buy/{itemId}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResult<BuyResult>>();
        }
    }
}
