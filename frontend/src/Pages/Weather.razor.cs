using MudBlazor;

namespace WeatherApp.Frontend.Pages;

public partial class Weather
{
    private readonly HourlyMock[] hourly =
[
    new("16:00", 12, 10, 18),
    new("19:00", 9, 20, 12),
    new("22:00", 9, 25, 10),
    new("01:00", 8, 15, 9),
    new("04:00", 8, 10, 8),
    new("07:00", 8, 5, 9),
    new("10:00", 8, 8, 11),
    new("13:00", 12, 12, 14),
];

    private readonly DailyMock[] daily =
    [
        new("Mon", Icons.Material.Filled.Cloud, 12, 6),
    new("Tue", Icons.Material.Filled.WbSunny, 13, 4),
    new("Wed", Icons.Material.Filled.WbSunny, 13, 4),
    new("Thu", Icons.Material.Filled.WbSunny, 16, 4),
    new("Fri", Icons.Material.Filled.Umbrella, 16, 4),
    new("Sat", Icons.Material.Filled.Grain, 11, 4),
    new("Sun", Icons.Material.Filled.WbSunny, 12, 3),
    new("Mon", Icons.Material.Filled.WbCloudy, 14, 3),
];

    private record HourlyMock(string Time, int Temp, int Precip, int Wind);

    private record DailyMock(string Day, string Icon, int High, int Low);
}
