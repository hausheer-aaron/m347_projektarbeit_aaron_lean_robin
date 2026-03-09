namespace WeatherApp.API.Models;

public sealed class ScheduleResponse
{
    public string Id { get; set; } = string.Empty;
    public string Day { get; set; } = string.Empty;
    public List<ScheduleItemResponse> Items { get; set; } = new List<ScheduleItemResponse>();
}