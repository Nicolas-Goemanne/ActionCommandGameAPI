using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class PositiveGameEventSdk
    {
        private readonly HttpClient _httpClient;

        public PositiveGameEventSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> GetRandomPositiveGameEventAsync(bool hasAttackItem)
        {
            var response = await _httpClient.GetAsync($"/api/positivegameevent/random?hasAttackItem={hasAttackItem}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResult<PositiveGameEventResult>>();
        }
    }
}
