namespace WeatherApp.Frontend.Models;

public sealed class CurrentWeatherItem
{
    public DateTime Time { get; set; }
    public double? Temperature { get; set; }
    public int? WeatherCode { get; set; }
    public bool IsDay { get; set; }
}