using System.Globalization;
using System.Net.Http.Json;
using System.Web;
using WeatherApp.Frontend.DTOs;
using WeatherApp.Frontend.Models;
using WeatherApp.Frontend.Models.OpenMeteo.App;
using WeatherApp.Frontend.Services.Interfaces;

namespace WeatherApp.Frontend.Services;

public sealed class OpenMeteoService : IOpenMeteoService
{
    private const string GeocodingBaseUrl = "https://geocoding-api.open-meteo.com/v1/search";
    private const string ForecastBaseUrl = "https://api.open-meteo.com/v1/forecast";

    private readonly HttpClient _httpClient;

    public OpenMeteoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LocationSearchItem>> SearchLocationsAsync(string searchText, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return new List<LocationSearchItem>();
        }

        string url = $"{GeocodingBaseUrl}?name={HttpUtility.UrlEncode(searchText)}&count=10&language=de&format=json";

        OpenMeteoGeocodingResponse? response = await _httpClient.GetFromJsonAsync<OpenMeteoGeocodingResponse>(url, cancellationToken);

        if (response?.Results is null || response.Results.Count == 0)
        {
            return new List<LocationSearchItem>();
        }

        return response.Results.Select(MapLocation).ToList();
    }

    public async Task<WeatherForecastViewModel?> GetForecastAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
    {
        string url =
            $"{ForecastBaseUrl}" +
            $"?latitude={latitude.ToString(CultureInfo.InvariantCulture)}" +
            $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}" +
            $"&current=temperature_2m,weather_code,is_day" +
            $"&hourly=temperature_2m,precipitation_probability,precipitation,weather_code,wind_speed_10m" +
            $"&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,weather_code" +
            $"&timezone=auto";

        OpenMeteoForecastResponse? response = await _httpClient.GetFromJsonAsync<OpenMeteoForecastResponse>(url, cancellationToken);

        if (response is null)
        {
            return null;
        }

        return new WeatherForecastViewModel
        {
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            Timezone = response.Timezone,
            Current = MapCurrent(response.Current),
            Hourly = MapHourly(response.Hourly),
            Daily = MapDaily(response.Daily)
        };
    }

    private static LocationSearchItem MapLocation(OpenMeteoLocationDTO location)
    {
        string region = location.Admin1 ?? location.Admin2 ?? location.Admin3 ?? location.Admin4 ?? string.Empty;

        string displayName = string.IsNullOrWhiteSpace(region)
            ? $"{location.Name}, {location.Country}"
            : $"{location.Name}, {region}, {location.Country}";

        return new LocationSearchItem
        {
            Id = location.Id,
            Name = location.Name,
            DisplayName = displayName,
            Country = location.Country,
            Region = region,
            Timezone = location.Timezone,
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };
    }

    private static CurrentWeatherItem? MapCurrent(OpenMeteoCurrentDTO? current)
    {
        if (current is null)
        {
            return null;
        }

        return new CurrentWeatherItem
        {
            Time = current.Time,
            Temperature = current.Temperature2m,
            WeatherCode = current.WeatherCode,
            IsDay = current.IsDay == 1
        };
    }

    private static List<HourlyWeatherItem> MapHourly(OpenMeteoHourlyDTO? hourly)
    {
        if (hourly?.Time is null || hourly.Time.Count == 0)
        {
            return new List<HourlyWeatherItem>();
        }

        List<HourlyWeatherItem> items = new();

        for (int i = 0; i < hourly.Time.Count; i++)
        {
            items.Add(new HourlyWeatherItem
            {
                Time = hourly.Time[i],
                Temperature = GetValue(hourly.Temperature2m, i),
                PrecipitationProbability = GetValue(hourly.PrecipitationProbability, i),
                Precipitation = GetValue(hourly.Precipitation, i),
                WeatherCode = GetValue(hourly.WeatherCode, i),
                WindSpeed = GetValue(hourly.WindSpeed10m, i)
            });
        }

        return items;
    }

    private static List<DailyWeatherItem> MapDaily(OpenMeteoDailyDto? daily)
    {
        if (daily?.Time is null || daily.Time.Count == 0)
        {
            return new List<DailyWeatherItem>();
        }

        List<DailyWeatherItem> items = new();

        for (int i = 0; i < daily.Time.Count; i++)
        {
            items.Add(new DailyWeatherItem
            {
                Date = daily.Time[i],
                TemperatureMax = GetValue(daily.Temperature2mMax, i),
                TemperatureMin = GetValue(daily.Temperature2mMin, i),
                Sunrise = GetValue(daily.Sunrise, i),
                Sunset = GetValue(daily.Sunset, i),
                WeatherCode = GetValue(daily.WeatherCode, i)
            });
        }

        return items;
    }

    private static T? GetValue<T>(IReadOnlyList<T?>? list, int index)
    {
        if (list is null || index < 0 || index >= list.Count)
        {
            return default;
        }

        return list[index];
    }
}