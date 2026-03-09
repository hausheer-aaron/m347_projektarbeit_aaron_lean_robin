using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WeatherApp.API.Models;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Controllers;

[ApiController]
[Route("api/schedule")]
public sealed class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public IActionResult GetSchedule()
    {
        try
        {
            IReadOnlyList<ScheduleResponse> schedule = _scheduleService.GetSchedule();
            return Ok(schedule);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpGet("weekdays")]
    public IActionResult GetWeekdays()
    {
        try
        {
            IReadOnlyList<Weekday> weekdays = _scheduleService.GetWeekdays();
            return Ok(weekdays);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] string id)
    {
        try
        {
            Absence? item = _scheduleService.GetById(id);
            if (item == null)
            {
                return NotFound(new { message = "Schedule entry not found" });
            }

            return Ok(item);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] Absence absence)
    {
        try
        {
            Absence created = _scheduleService.Create(absence);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] string id, [FromBody] Absence absence)
    {
        try
        {
            Absence? updated = _scheduleService.Update(id, absence);
            if (updated == null)
            {
                return NotFound(new { message = "Schedule entry not found" });
            }

            return Ok(updated);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
        try
        {
            bool deleted = _scheduleService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = "Schedule entry not found" });
            }

            return Ok(new { message = "Schedule entry deleted" });
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }
}