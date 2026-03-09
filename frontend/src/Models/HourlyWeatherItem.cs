namespace WeatherApp.Frontend.Models;

public sealed class HourlyWeatherItem
{
    public DateTime Time { get; set; }
    public double? Temperature { get; set; }
    public int? PrecipitationProbability { get; set; }
    public double? Precipitation { get; set; }
    public int? WeatherCode { get; set; }
    public double? WindSpeed { get; set; }
}