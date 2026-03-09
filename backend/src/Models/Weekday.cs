using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherApp.API.Models;

public sealed class Weekday
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("day")]
    public string Day { get; set; } = string.Empty;
}