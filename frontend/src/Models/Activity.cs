namespace WeatherApp.Frontend.Models;

public class Activity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public TimeOnly From { get; set; } = new(9, 0);
    public TimeOnly To { get; set; } = new(10, 0);
    public bool IsWeekly { get; set; }

    /// <summary>
    /// For one-time activities: the specific date.
    /// For weekly activities: the DayOfWeek is derived from this date.
    /// </summary>
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public DayOfWeek DayOfWeek => Date.DayOfWeek;
}
