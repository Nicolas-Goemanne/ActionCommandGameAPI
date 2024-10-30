using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class NegativeGameEventSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NegativeGameEventSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<NegativeGameEventResult>> GetRandomNegativeGameEvent()
        {
            var httpClient = CreateHttpClient();
            var route = "/api/negativegameevent/random";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<NegativeGameEventResult>>();
            if (result is null)
            {
                return new ServiceResult<NegativeGameEventResult>();
            }

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient("ActionCommandGameApi");
        }
    }
}

