using System.Text.Json;

namespace WeatherApp.API.Services.Interfaces;

public interface IOpenMeteoService
{
    Task<JsonDocument> GetForecastAsync(double lat, double lon, CancellationToken cancellationToken);

    Task<JsonDocument> GeocodeAsync(string name, CancellationToken cancellationToken);
}