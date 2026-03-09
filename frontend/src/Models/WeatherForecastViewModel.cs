namespace WeatherApp.Frontend.Models;

public sealed class WeatherForecastViewModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public CurrentWeatherItem? Current { get; set; }
    public List<HourlyWeatherItem> Hourly { get; set; } = new();
    public List<DailyWeatherItem> Daily { get; set; } = new();
}