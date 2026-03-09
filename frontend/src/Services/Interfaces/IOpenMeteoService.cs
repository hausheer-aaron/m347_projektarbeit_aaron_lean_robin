using WeatherApp.Frontend.Models;
using WeatherApp.Frontend.Models.OpenMeteo.App;

namespace WeatherApp.Frontend.Services.Interfaces;

public interface IOpenMeteoService
{
    Task<List<LocationSearchItem>> SearchLocationsAsync(string searchText, CancellationToken cancellationToken = default);

    Task<WeatherForecastViewModel?> GetForecastAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
}