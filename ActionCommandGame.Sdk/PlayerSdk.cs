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
    private readonly IHttpClientFactory _httpClientFactory;

    public PlayerSdk(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ServiceResult<PlayerResult>> Get(int id)
    {
        var httpClient = CreateHttpClient();
        var route = $"api/Player/{id}";

        var response = await httpClient.GetAsync(route);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<PlayerResult>>();
        if (result is null)
        {
            return new ServiceResult<PlayerResult>();
        }

        return result;
    }

    public async Task<ServiceResult<IList<PlayerResult>>> Find(PlayerFilter? filter)
    {
        var httpClient = CreateHttpClient();
        var route = "api/Player";

        var response = await httpClient.GetAsync(route);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerResult>>>();
        if (result is null)
        {
            return new ServiceResult<IList<PlayerResult>>();
        }

        return result;
    }

   
    private HttpClient CreateHttpClient()
    {
        return _httpClientFactory.CreateClient("ActionCommandGameApi");
    }
}
