using System.Text.Json.Serialization;

namespace WeatherApp.Frontend.DTOs;

public sealed class OpenMeteoHourlyDTO
{
    [JsonPropertyName("time")]
    public List<DateTime>? Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public List<double?>? Temperature2m { get; set; }

    [JsonPropertyName("precipitation_probability")]
    public List<int?>? PrecipitationProbability { get; set; }

    [JsonPropertyName("precipitation")]
    public List<double?>? Precipitation { get; set; }

    [JsonPropertyName("weather_code")]
    public List<int?>? WeatherCode { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public List<double?>? WindSpeed10m { get; set; }
}