using System.Net;
using System.Net.Http.Json;
using WeatherApp.Frontend.Models;
using WeatherApp.Frontend.Services.Interfaces;

namespace WeatherApp.Frontend.Services;

public sealed class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;
    private const string BasePath = "api/location";

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LocationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<LocationDTO>>(BasePath, cancellationToken) ?? [];
    }

    public async Task<LocationDTO?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{BasePath}/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LocationDTO>(cancellationToken: cancellationToken);
    }

    public async Task<LocationDTO> CreateAsync(LocationDTO location, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BasePath, location, cancellationToken);
        response.EnsureSuccessStatusCode();

        LocationDTO? created = await response.Content.ReadFromJsonAsync<LocationDTO>(cancellationToken: cancellationToken);
        return created ?? throw new InvalidOperationException("Create returned empty response body.");
    }

    public async Task<LocationDTO?> UpdateAsync(string id, LocationDTO location, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{BasePath}/{id}", location, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LocationDTO>(cancellationToken: cancellationToken);
    }
}