using WeatherApp.Frontend.Models;

namespace WeatherApp.Frontend.Services.Interfaces;

public interface ILocationService
{
    Task<List<LocationDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LocationDTO?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<LocationDTO> CreateAsync(LocationDTO location, CancellationToken cancellationToken = default);
    Task<LocationDTO?> UpdateAsync(string id, LocationDTO location, CancellationToken cancellationToken = default);
}