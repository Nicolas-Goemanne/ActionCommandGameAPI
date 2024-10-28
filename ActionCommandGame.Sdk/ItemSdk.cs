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
        private readonly HttpClient _httpClient;

        public ItemSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<ItemResult>> GetItemAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/item/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResult<ItemResult>>();
        }

        public async Task<ServiceResult<IList<ItemResult>>> GetAllItemsAsync()
        {
            var response = await _httpClient.GetAsync("/api/item");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceResult<IList<ItemResult>>>();
        }
    }
}
