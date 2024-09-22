using System.Text;
using System.Text.Json;

namespace BuildingBlocks.Shared.Infrastructure;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse);
    }

    public async Task<T?> PostAsync<T>(string? uri, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse);
    }

    public async Task<T?> PutAsync<T>(string uri, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(uri, content);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse);
    }

    public async Task DeleteAsync(string uri)
    {
        var response = await _httpClient.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();
    }
}
