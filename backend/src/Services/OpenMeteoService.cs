using System.Globalization;
using System.Text.Json;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Services;

public sealed class OpenMeteoService : IOpenMeteoService
{
    private readonly HttpClient _httpClient;

    public OpenMeteoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JsonDocument> GetForecastAsync(double lat, double lon, CancellationToken cancellationToken)
    {
        string latStr = lat.ToString(CultureInfo.InvariantCulture);
        string lonStr = lon.ToString(CultureInfo.InvariantCulture);

        string url =
            "https://api.open-meteo.com/v1/forecast" +
            $"?latitude={latStr}" +
            $"&longitude={lonStr}" +
            "&hourly=temperature_2m,precipitation" +
            "&timezone=Europe%2FZurich";

        using HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        JsonDocument json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return json;
    }

    public async Task<JsonDocument> GeocodeAsync(string name, CancellationToken cancellationToken)
    {
        string encodedName = Uri.EscapeDataString(name);

        string url =
            "https://geocoding-api.open-meteo.com/v1/search" +
            $"?name={encodedName}" +
            "&count=5&language=en&format=json";

        using HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        JsonDocument json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return json;
    }
}