using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

public class PlayerSdk
{
    private readonly HttpClient _httpClient;

    public PlayerSdk(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResult<PlayerResult>> GetPlayerAsync(int playerId)
    {
        var response = await _httpClient.GetAsync($"/api/player/{playerId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<PlayerResult>>();
    }

    public async Task<ServiceResult<IList<PlayerResult>>> GetAllPlayersAsync(PlayerFilter? filter)
    {
        var query = filter != null ? $"?filterUserPlayers={filter.FilterUserPlayers}" : string.Empty;
        var response = await _httpClient.GetAsync($"/api/player{query}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerResult>>>();
    }
}
