using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherApp.API.Models;

public sealed class AbsenceTimeRange
{
    [BsonElement("start")]
    public DateTime Start { get; set; }

    [BsonElement("end")]
    public DateTime End { get; set; }

    [BsonElement("place_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PlaceId { get; set; } = string.Empty;

    [BsonElement("recurring")]
    public bool Recurring { get; set; }
}