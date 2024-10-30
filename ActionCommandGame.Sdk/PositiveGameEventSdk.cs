using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class PositiveGameEventSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PositiveGameEventSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> GetRandomPositiveGameEventAsync(bool hasAttackItem)
        {
            var httpClient = CreateHttpClient();
            var route = $"/api/positivegameevent/random?hasAttackItem={hasAttackItem}";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PositiveGameEventResult>>();
            if (result is null)
            {
                return new ServiceResult<PositiveGameEventResult>();
            }

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient("ActionCommandGameApi");
        }
    }
}

