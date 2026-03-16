using WeatherApp.Frontend.Models;

namespace WeatherApp.Frontend.Services.Interfaces;

public interface IScheduleService
{
    Task<List<ScheduleResponseDTO>> GetScheduleAsync(CancellationToken cancellationToken = default);
    Task<List<WeekdayDTO>> GetWeekdaysAsync(CancellationToken cancellationToken = default);
    Task<AbsenceDTO?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<AbsenceDTO> CreateAsync(AbsenceDTO absence, CancellationToken cancellationToken = default);
    Task<AbsenceDTO?> UpdateAsync(string id, AbsenceDTO absence, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}