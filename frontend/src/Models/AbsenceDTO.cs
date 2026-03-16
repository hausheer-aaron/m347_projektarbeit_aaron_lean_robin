namespace WeatherApp.Frontend.Models;

public sealed class AbsenceDTO
{
    public string? Id { get; set; }
    public string DayId { get; set; } = string.Empty;
    public List<AbsenceTimeRangeDTO> Ranges { get; set; } = [];
}