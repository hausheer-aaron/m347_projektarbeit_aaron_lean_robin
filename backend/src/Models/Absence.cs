using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherApp.API.Models;

public sealed class Absence
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("day_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string DayId { get; set; } = string.Empty;

    [BsonElement("ranges")]
    public List<AbsenceTimeRange> Ranges { get; set; } = new List<AbsenceTimeRange>();
}