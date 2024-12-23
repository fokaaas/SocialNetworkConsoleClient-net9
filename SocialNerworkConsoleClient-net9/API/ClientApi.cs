using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SocialNerworkConsoleClient_net9.Interfaces;

namespace SocialNerworkConsoleClient_net9.API;

public class ClientApi
{
    private readonly HttpClient _httpClient;
    protected readonly string BaseUrl;

    public ClientApi(string baseUrl)
    {
        BaseUrl = baseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }
    
    private void AddAuthorizationHeader()
    {
        var token = AuthManager.GetAuthToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    protected async Task<T> GetAsync<T>(string endpoint)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json);
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        AddAuthorizationHeader();
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(json);
    }

    protected async Task<TResponse> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        AddAuthorizationHeader();
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Patch, endpoint) { Content = content };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(json);
    }

    protected async Task DeleteAsync(string endpoint)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }
}