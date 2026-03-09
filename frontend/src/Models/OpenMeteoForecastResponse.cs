using System.Text.Json.Serialization;
using WeatherApp.Frontend.DTOs;

namespace WeatherApp.Frontend.Models;

public sealed class OpenMeteoForecastResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    [JsonPropertyName("current")]
    public OpenMeteoCurrentDTO? Current { get; set; }

    [JsonPropertyName("hourly")]
    public OpenMeteoHourlyDTO? Hourly { get; set; }

    [JsonPropertyName("daily")]
    public OpenMeteoDailyDto? Daily { get; set; }
}