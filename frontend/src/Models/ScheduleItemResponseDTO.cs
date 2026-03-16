namespace WeatherApp.Frontend.Models;

public sealed class ScheduleItemResponseDTO
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string PlaceName { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool Recurring { get; set; }
}