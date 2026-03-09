using WeatherApp.API.Models;

namespace WeatherApp.API.Services.Interfaces;

public interface ILocationService
{
    Location Create(Location dto);
    Location? Update(string id, Location dto);
    Location? Get(string id);
    IReadOnlyList<Location> GetAll();
}