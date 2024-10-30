using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

public class PlayerItemSdk
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PlayerItemSdk(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ServiceResult<PlayerItemResult>> Get(int id)
    {
        var httpClient = CreateHttpClient();
        var route = $"/api/playeritem/{id}";

        var response = await httpClient.GetAsync(route);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<PlayerItemResult>>();
        if (result is null)
        {
            return new ServiceResult<PlayerItemResult>();
        }

        return result;
    }

    public async Task<ServiceResult<IList<PlayerItemResult>>> Find(PlayerItemFilter filter)
    {
        var httpClient = CreateHttpClient();
        var query = filter.PlayerId.HasValue ? $"?playerId={filter.PlayerId}" : string.Empty;
        var route = $"/api/playeritem{query}";

        var response = await httpClient.GetAsync(route);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerItemResult>>>();
        if (result is null)
        {
            return new ServiceResult<IList<PlayerItemResult>>();
        }

        return result;
    }

    public async Task<ServiceResult<PlayerItemResult>> Create(int playerId, int itemId)
    {
        var httpClient = CreateHttpClient();
        var route = $"/api/playeritem/{playerId}/{itemId}";

        var response = await httpClient.PostAsync(route, null);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<PlayerItemResult>>();
        if (result is null)
        {
            return new ServiceResult<PlayerItemResult>();
        }

        return result;
    }

    public async Task Delete(int id)
    {
        var httpClient = CreateHttpClient();
        var route = $"/api/playeritem/{id}";

        var response = await httpClient.DeleteAsync(route);
        response.EnsureSuccessStatusCode();
    }

    private HttpClient CreateHttpClient()
    {
        return _httpClientFactory.CreateClient("ActionCommandGameApi");
    }
}
