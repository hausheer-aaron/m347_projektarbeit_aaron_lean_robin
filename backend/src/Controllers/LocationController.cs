using Microsoft.AspNetCore.Mvc;
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
        LocationItem created = _store.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] Location dto)
    {
        LocationItem? updated = _store.Update(id, dto);
        if (updated == null)
        {
            return NotFound(new { message = "Location not found" });
        }

        return Ok(updated);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IReadOnlyList<LocationItem> all = _store.GetAll();
        return Ok(all);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        LocationItem? item = _store.Get(id);
        if (item == null)
        {
            return NotFound(new { message = "Location not found" });
        }

        return Ok(item);
    }
}