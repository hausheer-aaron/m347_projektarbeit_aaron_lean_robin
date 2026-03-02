namespace WeatherApp.API.Models;

public sealed class Location
{
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}