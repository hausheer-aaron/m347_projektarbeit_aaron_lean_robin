using MongoDB.Driver;
using WeatherApp.API.Models;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Services;

public sealed class LocationService : ILocationService
{
    private readonly IMongoCollection<Location> _collection;

    public LocationService(IMongoCollection<Location> collection)
    {
        _collection = collection;
    }

    public Location Create(Location dto)
    {
        Location item = new Location
        {
            Name = dto.Name,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };

        _collection.InsertOne(item);
        return item;
    }

    public Location? Update(string id, Location dto)
    {
        FilterDefinition<Location> filter = Builders<Location>.Filter.Eq(x => x.Id, id);

        UpdateDefinition<Location> update = Builders<Location>.Update
            .Set(x => x.Name, dto.Name)
            .Set(x => x.Latitude, dto.Latitude)
            .Set(x => x.Longitude, dto.Longitude);

        FindOneAndUpdateOptions<Location> options = new FindOneAndUpdateOptions<Location>
        {
            ReturnDocument = ReturnDocument.After
        };

        return _collection.FindOneAndUpdate(filter, update, options);
    }

    public Location? Get(string id)
    {
        FilterDefinition<Location> filter = Builders<Location>.Filter.Eq(x => x.Id, id);
        return _collection.Find(filter).FirstOrDefault();
    }

    public IReadOnlyList<Location> GetAll()
    {
        return _collection.Find(Builders<Location>.Filter.Empty).ToList();
    }
}