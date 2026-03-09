namespace WeatherApp.Frontend.Models;

public sealed class DailyWeatherItem
{
    public DateTime Date { get; set; }
    public double? TemperatureMax { get; set; }
    public double? TemperatureMin { get; set; }
    public DateTime? Sunrise { get; set; }
    public DateTime? Sunset { get; set; }
    public int? WeatherCode { get; set; }
}