using System.Text.Json.Serialization;

namespace WeatherApp.Frontend.DTOs;

public sealed class OpenMeteoDailyDto
{
    [JsonPropertyName("time")]
    public List<DateTime>? Time { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double?>? Temperature2mMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double?>? Temperature2mMin { get; set; }

    [JsonPropertyName("sunrise")]
    public List<DateTime>? Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public List<DateTime>? Sunset { get; set; }

    [JsonPropertyName("weather_code")]
    public List<int?>? WeatherCode { get; set; }
}