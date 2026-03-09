using WeatherApp.API.Models;

namespace WeatherApp.API.Services.Interfaces;

public interface IScheduleService
{
    IReadOnlyList<ScheduleResponse> GetSchedule();
    IReadOnlyList<Weekday> GetWeekdays();
    Absence Create(Absence absence);
    Absence? Update(string id, Absence absence);
    Absence? GetById(string id);
    bool Delete(string id);
}