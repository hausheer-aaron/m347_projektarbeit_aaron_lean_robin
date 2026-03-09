using Microsoft.AspNetCore.Components;
using WeatherApp.Frontend.Models;
using WeatherApp.Frontend.Services;

namespace WeatherApp.Frontend.Pages;

public partial class Planner
{
    [Inject] private ActivityService ActivityService { get; set; } = default!;

    private List<Activity> activities = [];
    private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Now);
    private bool showForm;
    private Activity editActivity = new();
    private bool isEdit;
    private List<string> locationSuggestions = [];
    private bool showLocationDropdown;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadActivities();
            StateHasChanged();
        }
    }

    private async Task LoadActivities()
    {
        activities = await ActivityService.GetForDateAsync(selectedDate);
    }

    private async Task ChangeDate(int days)
    {
        selectedDate = selectedDate.AddDays(days);
        await LoadActivities();
    }

    private void StartNew()
    {
        editActivity = new Activity { Date = selectedDate };
        isEdit = false;
        showForm = true;
        showLocationDropdown = false;
    }

    private void StartEdit(Activity a)
    {
        editActivity = new Activity
        {
            Id = a.Id,
            Title = a.Title,
            Location = a.Location,
            From = a.From,
            To = a.To,
            IsWeekly = a.IsWeekly,
            Date = a.Date,
        };
        isEdit = true;
        showForm = true;
        showLocationDropdown = false;
    }

    private async Task SaveActivity()
    {
        if (string.IsNullOrWhiteSpace(editActivity.Title))
            return;

        await ActivityService.SaveAsync(editActivity);
        showForm = false;
        await LoadActivities();
    }

    private void CancelForm()
    {
        showForm = false;
    }

    private async Task DeleteActivity(Guid id)
    {
        await ActivityService.DeleteAsync(id);
        await LoadActivities();
    }

    private void OnLocationInput(ChangeEventArgs e)
    {
        editActivity.Location = e.Value?.ToString() ?? "";
        locationSuggestions = WeatherRecommendation.GetLocationSuggestions(editActivity.Location);
        showLocationDropdown = locationSuggestions.Count > 0;
    }

    private void SelectLocation(string loc)
    {
        editActivity.Location = loc;
        showLocationDropdown = false;
    }

    private static string GetActivityEmoji(Activity a) => a.Title.ToLower() switch
    {
        var t when t.Contains("sport") || t.Contains("lauf") || t.Contains("joggen") => "??",
        var t when t.Contains("spazier") || t.Contains("walk") => "??",
        var t when t.Contains("velo") || t.Contains("rad") || t.Contains("bike") => "??",
        var t when t.Contains("einkauf") || t.Contains("shop") => "??",
        var t when t.Contains("arbeit") || t.Contains("work") => "??",
        var t when t.Contains("schule") || t.Contains("uni") => "??",
        _ => "???",
    };
}
