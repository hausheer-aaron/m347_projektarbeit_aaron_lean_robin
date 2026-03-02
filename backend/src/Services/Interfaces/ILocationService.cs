using WeatherApp.API.Models;

namespace WeatherApp.API.Services.Interfaces;

public interface ILocationService
{
    LocationItem Create(Location dto);
    LocationItem? Update(Guid id, Location dto);
    LocationItem? Get(Guid id);
    IReadOnlyList<LocationItem> GetAll();
}