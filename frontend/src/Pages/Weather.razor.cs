using Microsoft.AspNetCore.Components;
using WeatherApp.Frontend.Models;
using WeatherApp.Frontend.Services;
using WeatherApp.Frontend.Services.Interfaces;

namespace WeatherApp.Frontend.Pages;

public partial class Weather
{
    [Inject] private IOpenMeteoService MeteoService { get; set; } = default!;
    [Inject] private ActivityService ActivityService { get; set; } = default!;

    private WeatherForecastViewModel? forecast;
    private string locationName = "Zürich";
    private bool isLoading = true;
    private int selectedDayIndex;
    private int activeTab;
    private bool showMyDayOverlay;
    private List<Activity> todayActivities = [];

    private double lat = 47.3769;
    private double lon = 8.5417;

    protected override async Task OnInitializedAsync()
    {
        await LoadForecast();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            todayActivities = await ActivityService.GetForDateAsync(DateOnly.FromDateTime(DateTime.Now));
            StateHasChanged();
        }
    }

    private async Task LoadForecast()
    {
        isLoading = true;
        forecast = await MeteoService.GetForecastAsync(lat, lon);
        isLoading = false;
    }

    private void SelectDay(int index)
    {
        selectedDayIndex = index;
    }

    private int CurrentTemp => (int)Math.Round(forecast?.Current?.Temperature ?? 0);
    private string CurrentCondition => GetConditionText(forecast?.Current?.WeatherCode ?? 0);

    private List<HourlyView> GetHourlyData()
    {
        if (forecast?.Hourly is null || forecast.Daily is null || selectedDayIndex >= forecast.Daily.Count)
            return [];

        var selectedDay = forecast.Daily[selectedDayIndex].Date.Date;
        var isToday = selectedDay.Date == DateTime.Now.Date;

        var startHour = isToday
            ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0)
            : selectedDay;
        var endOfDay = selectedDay.AddDays(1);

        return forecast.Hourly
            .Where(h => h.Time >= startHour && h.Time < endOfDay)
            .Select(h => new HourlyView(
                h.Time.ToString("HH:mm"),
                (int)Math.Round(h.Temperature ?? 0),
                h.PrecipitationProbability ?? 0,
                (int)Math.Round(h.WindSpeed ?? 0),
                h.WeatherCode ?? 0
            ))
            .ToList();
    }

    private List<DailyView> GetDailyData()
    {
        if (forecast?.Daily is null) return [];

        return forecast.Daily
            .Take(7)
            .Select((d, i) => new DailyView(
                i == 0 ? "Heute" : d.Date.ToString("ddd"),
                d.Date.ToString("dd.MM"),
                (int)Math.Round(d.TemperatureMax ?? 0),
                (int)Math.Round(d.TemperatureMin ?? 0),
                d.WeatherCode ?? 0
            ))
            .ToList();
    }

    private DailyWeatherItem? SelectedDayData =>
        forecast?.Daily is not null && selectedDayIndex < forecast.Daily.Count
            ? forecast.Daily[selectedDayIndex]
            : null;

    // --- Emoji weather icons ---

    internal static string GetWeatherEmoji(int code) => code switch
    {
        0 => "☀️",
        1 => "🌤️",
        2 => "⛅",
        3 => "☁️",
        45 or 48 => "🌫️",
        51 or 53 or 55 => "🌦️",
        56 or 57 => "🌧️",
        61 or 63 => "🌧️",
        65 => "🌧️",
        66 or 67 => "🧊",
        71 or 73 => "🌨️",
        75 or 77 => "❄️",
        80 or 81 or 82 => "🌦️",
        85 or 86 => "🌨️",
        95 => "⛈️",
        96 or 99 => "⛈️",
        _ => "☁️",
    };

    internal static string GetConditionText(int code) => code switch
    {
        0 => "Klar",
        1 => "Überwiegend klar",
        2 => "Teilweise bewölkt",
        3 => "Bewölkt",
        45 or 48 => "Nebel",
        51 or 53 or 55 => "Nieselregen",
        56 or 57 => "Gefrierender Nieselregen",
        61 or 63 or 65 => "Regen",
        66 or 67 => "Gefrierender Regen",
        71 or 73 or 75 => "Schneefall",
        77 => "Schneekörner",
        80 or 81 or 82 => "Regenschauer",
        85 or 86 => "Schneeschauer",
        95 => "Gewitter",
        96 or 99 => "Gewitter mit Hagel",
        _ => "Unbekannt",
    };

    private record HourlyView(string Time, int Temp, int Precip, int Wind, int Code);
    private record DailyView(string Day, string DateLabel, int High, int Low, int Code);

    // --- MyDay overlay ---

    private void OpenMyDayOverlay() => showMyDayOverlay = true;
    private void CloseMyDayOverlay() => showMyDayOverlay = false;

    private List<WeatherRecommendation.Recommendation> GetPackList()
    {
        if (forecast?.Hourly is null) return [];

        var all = new List<WeatherRecommendation.Recommendation>();
        foreach (var a in todayActivities)
        {
            all.AddRange(WeatherRecommendation.GetRecommendations(a, forecast.Hourly));
        }
        return all.DistinctBy(r => r.Label).ToList();
    }

    private string GetActivityWeatherSummary(Activity a)
    {
        if (forecast?.Hourly is null) return "";
        return WeatherRecommendation.GetWeatherSummary(a, forecast.Hourly);
    }

    private List<WeatherRecommendation.Recommendation> GetActivityRecommendations(Activity a)
    {
        if (forecast?.Hourly is null) return [];
        return WeatherRecommendation.GetRecommendations(a, forecast.Hourly);
    }

    private static string GetActivityEmoji(string title) => title.ToLower() switch
    {
        var t when t.Contains("sport") || t.Contains("lauf") || t.Contains("joggen") => "🏃",
        var t when t.Contains("spazier") || t.Contains("walk") => "🌳",
        var t when t.Contains("velo") || t.Contains("rad") || t.Contains("bike") => "🚴",
        var t when t.Contains("einkauf") || t.Contains("shop") => "🛒",
        var t when t.Contains("arbeit") || t.Contains("work") => "💼",
        var t when t.Contains("schule") || t.Contains("uni") => "🎓",
        _ => "🌤️",
    };
}
