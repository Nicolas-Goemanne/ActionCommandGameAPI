using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

public class PlayerItemSdk
{
    private readonly HttpClient _httpClient;

    public PlayerItemSdk(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResult<PlayerItemResult>> GetPlayerItemAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/playeritem/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<PlayerItemResult>>();
    }

    public async Task<ServiceResult<IList<PlayerItemResult>>> GetAllPlayerItemsAsync(PlayerItemFilter filter)
    {
        var query = filter.PlayerId.HasValue ? $"?playerId={filter.PlayerId}" : string.Empty;
        var response = await _httpClient.GetAsync($"/api/playeritem{query}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerItemResult>>>();
    }

    public async Task<ServiceResult<PlayerItemResult>> CreatePlayerItemAsync(int playerId, int itemId)
    {
        var response = await _httpClient.PostAsync($"/api/playeritem/{playerId}/{itemId}", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<PlayerItemResult>>();
    }

    public async Task DeletePlayerItemAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/playeritem/{id}");
        response.EnsureSuccessStatusCode();
    }
}