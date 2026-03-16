namespace WeatherApp.Frontend.Models;

public sealed class ScheduleResponseDTO
{
    public string Id { get; set; } = string.Empty;
    public string Day { get; set; } = string.Empty;
    public List<ScheduleItemResponseDTO> Items { get; set; } = [];
}