using System.Net;
using System.Net.Http.Json;
using WeatherApp.Frontend.Models;
using WeatherApp.Frontend.Services.Interfaces;

namespace WeatherApp.Frontend.Services;

public sealed class ScheduleService : IScheduleService
{
    private readonly HttpClient _httpClient;
    private const string BasePath = "api/schedule";

    public ScheduleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ScheduleResponseDTO>> GetScheduleAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<ScheduleResponseDTO>>(BasePath, cancellationToken) ?? [];
    }

    public async Task<List<WeekdayDTO>> GetWeekdaysAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<WeekdayDTO>>($"{BasePath}/weekdays", cancellationToken) ?? [];
    }

    public async Task<AbsenceDTO?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{BasePath}/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AbsenceDTO>(cancellationToken: cancellationToken);
    }

    public async Task<AbsenceDTO> CreateAsync(AbsenceDTO absence, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BasePath, absence, cancellationToken);
        response.EnsureSuccessStatusCode();

        AbsenceDTO? created = await response.Content.ReadFromJsonAsync<AbsenceDTO>(cancellationToken: cancellationToken);
        return created ?? throw new InvalidOperationException("Create returned empty response body.");
    }

    public async Task<AbsenceDTO?> UpdateAsync(string id, AbsenceDTO absence, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{BasePath}/{id}", absence, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AbsenceDTO>(cancellationToken: cancellationToken);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{BasePath}/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return false;

        response.EnsureSuccessStatusCode();
        return true;
    }
}