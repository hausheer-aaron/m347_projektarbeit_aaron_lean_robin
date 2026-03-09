using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WeatherApp.API.Models;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Controllers;

[ApiController]
[Route("api/location")]
public sealed class LocationController : ControllerBase
{
    private readonly ILocationService _store;

    public LocationController(ILocationService store)
    {
        _store = store;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Location dto)
    {
        try
        {
            Location created = _store.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] string id, [FromBody] Location dto)
    {
        try
        {
            Location? updated = _store.Update(id, dto);
            if (updated == null)
            {
                return NotFound(new { message = "Location not found" });
            }

            return Ok(updated);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            IReadOnlyList<Location> all = _store.GetAll();
            return Ok(all);
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
            Location? item = _store.Get(id);
            if (item == null)
            {
                return NotFound(new { message = "Location not found" });
            }

            return Ok(item);
        }
        catch (MongoException)
        {
            return StatusCode(500, new { message = "Database not reachable" });
        }
    }
}