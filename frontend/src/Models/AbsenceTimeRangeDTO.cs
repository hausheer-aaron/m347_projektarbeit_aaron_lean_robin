namespace WeatherApp.Frontend.Models;

public sealed class AbsenceTimeRangeDTO
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string PlaceId { get; set; } = string.Empty;
    public bool Recurring { get; set; }
}