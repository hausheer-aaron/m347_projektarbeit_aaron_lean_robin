namespace WeatherApp.Frontend.Models.OpenMeteo.App;

public sealed class LocationSearchItem
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? Timezone { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}