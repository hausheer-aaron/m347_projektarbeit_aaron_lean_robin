using System.Text.Json.Serialization;
using WeatherApp.Frontend.DTOs;

namespace WeatherApp.Frontend.Models;

public sealed class OpenMeteoGeocodingResponse
{
    [JsonPropertyName("results")]
    public List<OpenMeteoLocationDTO>? Results { get; set; }
}