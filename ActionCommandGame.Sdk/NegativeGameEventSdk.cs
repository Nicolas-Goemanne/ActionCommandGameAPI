using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class NegativeGameEventSdk
    {
        private readonly HttpClient _httpClient;

        public NegativeGameEventSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<NegativeGameEventResult>> GetRandomNegativeGameEventAsync()
        {
            var response = await _httpClient.GetAsync("/api/negativegameevent/random");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResult<NegativeGameEventResult>>();
        }
    }
}
