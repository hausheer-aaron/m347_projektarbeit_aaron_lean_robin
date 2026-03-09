using Microsoft.JSInterop;
using System.Text.Json;
using WeatherApp.Frontend.Models;

namespace WeatherApp.Frontend.Services;

public class ActivityService
{
    private const string StorageKey = "weatherapp_activities";
    private readonly IJSRuntime _js;
    private List<Activity>? _cache;

    public ActivityService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<List<Activity>> GetAllAsync()
    {
        if (_cache is not null)
            return _cache;

        var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
        if (string.IsNullOrEmpty(json))
        {
            _cache = [];
            return _cache;
        }

        _cache = JsonSerializer.Deserialize<List<Activity>>(json) ?? [];
        return _cache;
    }

    public async Task<List<Activity>> GetForDateAsync(DateOnly date)
    {
        var all = await GetAllAsync();
        return all
            .Where(a =>
                (a.IsWeekly && a.DayOfWeek == date.DayOfWeek) ||
                (!a.IsWeekly && a.Date == date))
            .OrderBy(a => a.From)
            .ToList();
    }

    public async Task SaveAsync(Activity activity)
    {
        var all = await GetAllAsync();
        var existing = all.FindIndex(a => a.Id == activity.Id);
        if (existing >= 0)
            all[existing] = activity;
        else
            all.Add(activity);

        await PersistAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var all = await GetAllAsync();
        all.RemoveAll(a => a.Id == id);
        await PersistAsync();
    }

    private async Task PersistAsync()
    {
        var json = JsonSerializer.Serialize(_cache);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }
}
