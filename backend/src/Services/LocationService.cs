using WeatherApp.API.Models;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Services;

public sealed class LocationService : ILocationService
{
    private readonly object _lockObj = new object();
    private readonly List<LocationItem> _items = new List<LocationItem>();

    public LocationItem Create(Location dto)
    {
        LocationItem item = new LocationItem
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            UpdatedAtUtc = DateTimeOffset.UtcNow
        };

        lock (_lockObj)
        {
            _items.Add(item);
        }

        return item;
    }

    public LocationItem? Update(Guid id, Location dto)
    {
        lock (_lockObj)
        {
            LocationItem? existing = _items.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return null;
            }

            existing.Name = dto.Name;
            existing.Latitude = dto.Latitude;
            existing.Longitude = dto.Longitude;
            existing.UpdatedAtUtc = DateTimeOffset.UtcNow;

            return existing;
        }
    }

    public LocationItem? Get(Guid id)
    {
        lock (_lockObj)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }
    }

    public IReadOnlyList<LocationItem> GetAll()
    {
        lock (_lockObj)
        {
            return _items.ToList();
        }
    }
}