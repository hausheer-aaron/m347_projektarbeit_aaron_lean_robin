namespace WeatherApp.Frontend.Models;

public sealed class LocationDTO
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}