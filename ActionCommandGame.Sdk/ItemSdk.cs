using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk
{
    public class ItemSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<ItemResult>> Get(int id)
        {
            var httpClient = CreateHttpClient();
            var route = $"/api/item/{id}";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ItemResult>>();
            if (result is null)
            {
                return new ServiceResult<ItemResult>();
            }

            return result;
        }

        public async Task<ServiceResult<IList<ItemResult>>> Find()
        {
            var httpClient = CreateHttpClient();
            var route = "/api/item";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<IList<ItemResult>>>();
            if (result is null)
            {
                return new ServiceResult<IList<ItemResult>>();
            }

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient("ActionCommandGameApi");
        }
    }
}

