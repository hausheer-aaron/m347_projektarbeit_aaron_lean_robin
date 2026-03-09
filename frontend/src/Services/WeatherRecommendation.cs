using WeatherApp.Frontend.Models;

namespace WeatherApp.Frontend.Services;

public static class WeatherRecommendation
{
    public record Recommendation(string Emoji, string Label);

    /// <summary>
    /// Mock location coordinates for weather lookup.
    /// </summary>
    public static readonly Dictionary<string, (double Lat, double Lon)> MockLocations = new()
    {
        ["Zug"] = (47.1724, 8.5172),
        ["Zürich"] = (47.3769, 8.5417),
        ["Bern"] = (46.9480, 7.4474),
        ["Luzern"] = (47.0502, 8.3093),
        ["Basel"] = (47.5596, 7.5886),
        ["St. Gallen"] = (47.4245, 9.3767),
        ["Winterthur"] = (47.5001, 8.7240),
        ["Baar"] = (47.1965, 8.5296),
        ["Cham"] = (47.1823, 8.4635),
        ["Rotkreuz"] = (47.1434, 8.4328),
    };

    public static List<string> GetLocationSuggestions(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length < 1)
            return [];

        return MockLocations.Keys
            .Where(l => l.Contains(input, StringComparison.OrdinalIgnoreCase))
            .OrderBy(l => l)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Graduated recommendations based on weather severity.
    /// </summary>
    public static List<Recommendation> GetRecommendations(
        Activity activity,
        List<HourlyWeatherItem> hourlyData)
    {
        var relevantHours = hourlyData
            .Where(h =>
            {
                var hour = TimeOnly.FromDateTime(h.Time);
                return hour >= activity.From && hour < activity.To;
            })
            .ToList();

        if (relevantHours.Count == 0)
            return [];

        var maxTemp = relevantHours.Max(h => h.Temperature ?? 0);
        var minTemp = relevantHours.Min(h => h.Temperature ?? 0);
        var avgTemp = relevantHours.Average(h => h.Temperature ?? 0);
        var maxPrecipProb = relevantHours.Max(h => h.PrecipitationProbability ?? 0);
        var maxPrecip = relevantHours.Max(h => h.Precipitation ?? 0);
        var maxWind = relevantHours.Max(h => h.WindSpeed ?? 0);
        var hasSun = relevantHours.Any(h => h.WeatherCode is 0 or 1);
        var hasSnow = relevantHours.Any(h => h.WeatherCode is 71 or 73 or 75 or 77 or 85 or 86);
        var hasThunder = relevantHours.Any(h => h.WeatherCode is 95 or 96 or 99);

        var items = new List<Recommendation>();

        // --- Rain: graduated ---
        if (maxPrecipProb > 70 || maxPrecip > 2.0)
        {
            // Heavy rain
            items.Add(new("?", "Regenschirm"));
            items.Add(new("??", "Regenjacke"));
        }
        else if (maxPrecipProb > 50 || maxPrecip > 0.5)
        {
            // Moderate rain
            items.Add(new("??", "Regenjacke"));
        }
        else if (maxPrecipProb > 30)
        {
            // Slight chance — just a tip
            items.Add(new("??", "Leichte Jacke (evtl. Regen)"));
        }

        // --- Cold: graduated ---
        if (minTemp < 0)
        {
            items.Add(new("??", "Handschuhe"));
            items.Add(new("??", "Schal"));
            items.Add(new("??", "Winterschuhe"));
            items.Add(new("??", "Winterjacke"));
        }
        else if (minTemp < 5)
        {
            items.Add(new("??", "Handschuhe"));
            items.Add(new("??", "Warme Jacke"));
        }
        else if (minTemp < 10)
        {
            items.Add(new("??", "Jacke"));
        }
        else if (minTemp < 15)
        {
            items.Add(new("??", "Leichte Jacke"));
        }

        // --- Heat ---
        if (maxTemp > 30)
        {
            items.Add(new("??", "Viel Wasser"));
            items.Add(new("??", "Sonnencreme"));
            items.Add(new("???", "Kopfbedeckung"));
        }
        else if (maxTemp > 25)
        {
            items.Add(new("??", "Wasser"));
            items.Add(new("??", "Sonnencreme"));
        }

        // --- Sun ---
        if (hasSun && avgTemp > 12)
        {
            items.Add(new("???", "Sonnenbrille"));
        }

        // --- Wind: graduated ---
        if (maxWind > 50)
        {
            items.Add(new("???", "Sturm — bleib sicher!"));
        }
        else if (maxWind > 30)
        {
            items.Add(new("??", "Windjacke"));
        }

        // --- Snow ---
        if (hasSnow)
        {
            items.Add(new("??", "Winterschuhe"));
        }

        // --- Thunder ---
        if (hasThunder)
        {
            items.Add(new("??", "Gewitter — Vorsicht!"));
        }

        return items.DistinctBy(r => r.Label).ToList();
    }

    /// <summary>
    /// Short weather summary for an activity time slot.
    /// </summary>
    public static string GetWeatherSummary(Activity activity, List<HourlyWeatherItem> hourlyData)
    {
        var relevantHours = hourlyData
            .Where(h =>
            {
                var hour = TimeOnly.FromDateTime(h.Time);
                return hour >= activity.From && hour < activity.To;
            })
            .ToList();

        if (relevantHours.Count == 0)
            return "Keine Wetterdaten";

        var avgTemp = (int)Math.Round(relevantHours.Average(h => h.Temperature ?? 0));
        var maxPrecipProb = relevantHours.Max(h => h.PrecipitationProbability ?? 0);
        var maxWind = (int)Math.Round(relevantHours.Max(h => h.WindSpeed ?? 0));

        var parts = new List<string> { $"{avgTemp}°C" };

        if (maxPrecipProb > 30)
            parts.Add($"Regen {maxPrecipProb}%");
        if (maxWind > 20)
            parts.Add($"Wind {maxWind} km/h");

        return string.Join(" · ", parts);
    }
}
