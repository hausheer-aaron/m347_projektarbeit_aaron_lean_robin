namespace WeatherApp.API.Models;

public sealed class LocationItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}