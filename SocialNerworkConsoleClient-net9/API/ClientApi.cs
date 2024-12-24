using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SocialNerworkConsoleClient_net9.Models;

namespace SocialNerworkConsoleClient_net9.API;

public class ClientApi
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    protected readonly string BaseUrl;

    public ClientApi(string resource)
    {
        BaseUrl = $@"http://127.0.0.1:5020/api/{resource}/";
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }

    private void AddAuthorizationHeader()
    {
        var token = AuthManager.GetAuthToken();
        if (!string.IsNullOrEmpty(token))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected async Task<T> GetAsync<T>(string endpoint)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.GetAsync(endpoint);
        var json = await response.Content.ReadAsStringAsync();
        if (response.StatusCode is not HttpStatusCode.Created && response.StatusCode is not HttpStatusCode.OK)
        {
            var exception = JsonSerializer.Deserialize<ClientExceptionModel>(json, _jsonSerializerOptions);
            throw new Exception(exception.Message);
        }
        return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        AddAuthorizationHeader();
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        var json = await response.Content.ReadAsStringAsync();
        if (response.StatusCode is not HttpStatusCode.Created && response.StatusCode is not HttpStatusCode.OK)
        {
            var exception = JsonSerializer.Deserialize<ClientExceptionModel>(json, _jsonSerializerOptions);
            throw new Exception(exception.Message);
        }
        return JsonSerializer.Deserialize<TResponse>(json, _jsonSerializerOptions);
    }

    protected async Task<TResponse> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        AddAuthorizationHeader();
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Patch, endpoint) { Content = content };
        var response = await _httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        if (response.StatusCode is not HttpStatusCode.Created && response.StatusCode is not HttpStatusCode.OK)
        {
            var exception = JsonSerializer.Deserialize<ClientExceptionModel>(json, _jsonSerializerOptions);
            throw new Exception(exception.Message);
        }
        return JsonSerializer.Deserialize<TResponse>(json, _jsonSerializerOptions);
    }

    protected async Task DeleteAsync(string endpoint)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.DeleteAsync(endpoint);
        if (response.StatusCode is not HttpStatusCode.Created && response.StatusCode is not HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsStringAsync();
            var exception = JsonSerializer.Deserialize<ClientExceptionModel>(json, _jsonSerializerOptions);
            throw new Exception(exception.Message);
        }
        response.EnsureSuccessStatusCode();
    }
}