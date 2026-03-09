using System.Text.Json.Serialization;

namespace WeatherApp.Frontend.DTOs;

public sealed class OpenMeteoLocationDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("admin1")]
    public string? Admin1 { get; set; }

    [JsonPropertyName("admin2")]
    public string? Admin2 { get; set; }

    [JsonPropertyName("admin3")]
    public string? Admin3 { get; set; }

    [JsonPropertyName("admin4")]
    public string? Admin4 { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }
}