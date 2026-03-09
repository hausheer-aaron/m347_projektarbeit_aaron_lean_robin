using MongoDB.Driver;
using WeatherApp.API.Models;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Services;

public sealed class ScheduleService : IScheduleService
{
    private readonly IMongoCollection<Absence> _absences;
    private readonly IMongoCollection<Weekday> _weekdays;
    private readonly IMongoCollection<Location> _places;

    public ScheduleService(
        IMongoCollection<Absence> absences,
        IMongoCollection<Weekday> weekdays,
        IMongoCollection<Location> places)
    {
        _absences = absences;
        _weekdays = weekdays;
        _places = places;
    }

    public IReadOnlyList<ScheduleResponse> GetSchedule()
    {
        List<Absence> absences = _absences.Find(Builders<Absence>.Filter.Empty).ToList();
        List<Weekday> weekdays = _weekdays.Find(Builders<Weekday>.Filter.Empty).ToList();
        List<Location> places = _places.Find(Builders<Location>.Filter.Empty).ToList();

        List<ScheduleResponse> result = new List<ScheduleResponse>();

        foreach (Absence absence in absences)
        {
            Weekday? weekday = weekdays.FirstOrDefault(x => x.Id == absence.DayId);
            if (weekday == null)
            {
                continue;
            }

            ScheduleResponse response = new ScheduleResponse
            {
                Id = absence.Id ?? string.Empty,
                Day = weekday.Day
            };

            foreach (AbsenceTimeRange range in absence.Ranges)
            {
                Location? place = places.FirstOrDefault(x => x.Id == range.PlaceId);
                if (place == null)
                {
                    continue;
                }

                response.Items.Add(new ScheduleItemResponse
                {
                    Start = range.Start,
                    End = range.End,
                    PlaceName = place.Name,
                    Latitude = place.Latitude,
                    Longitude = place.Longitude,
                    Recurring = range.Recurring
                });
            }

            result.Add(response);
        }

        return result;
    }

    public IReadOnlyList<Weekday> GetWeekdays()
    {
        return _weekdays
            .Find(Builders<Weekday>.Filter.Empty)
            .ToList();
    }

    public Absence Create(Absence absence)
    {
        Absence item = new Absence
        {
            DayId = absence.DayId,
            Ranges = absence.Ranges
        };

        _absences.InsertOne(item);
        return item;
    }

    public Absence? Update(string id, Absence absence)
    {
        FilterDefinition<Absence> filter = Builders<Absence>.Filter.Eq(x => x.Id, id);

        UpdateDefinition<Absence> update = Builders<Absence>.Update
            .Set(x => x.DayId, absence.DayId)
            .Set(x => x.Ranges, absence.Ranges);

        FindOneAndUpdateOptions<Absence> options = new FindOneAndUpdateOptions<Absence>
        {
            ReturnDocument = ReturnDocument.After
        };

        return _absences.FindOneAndUpdate(filter, update, options);
    }

    public Absence? GetById(string id)
    {
        FilterDefinition<Absence> filter = Builders<Absence>.Filter.Eq(x => x.Id, id);
        return _absences.Find(filter).FirstOrDefault();
    }

    public bool Delete(string id)
    {
        FilterDefinition<Absence> filter = Builders<Absence>.Filter.Eq(x => x.Id, id);
        DeleteResult result = _absences.DeleteOne(filter);
        return result.DeletedCount > 0;
    }
}