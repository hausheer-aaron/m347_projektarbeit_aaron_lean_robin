using System.Text.Json.Serialization;

namespace WeatherApp.Frontend.DTOs;

public sealed class OpenMeteoCurrentDTO
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double? Temperature2m { get; set; }

    [JsonPropertyName("weather_code")]
    public int? WeatherCode { get; set; }

    [JsonPropertyName("is_day")]
    public int? IsDay { get; set; }
}