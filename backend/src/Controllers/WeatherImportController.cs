using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherApp.API.Services.Interfaces;

namespace WeatherApp.API.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherImportController : ControllerBase
{
    private readonly IOpenMeteoService _openMeteoService;

    public WeatherImportController(IOpenMeteoService openMeteoService)
    {
        _openMeteoService = openMeteoService;
    }

    [HttpGet("fetch")]
    public async Task<IActionResult> Fetch([FromQuery] double lat, [FromQuery] double lon, CancellationToken cancellationToken)
    {
        JsonDocument result = await _openMeteoService.GetForecastAsync(lat, lon, cancellationToken);

        return Ok(result.RootElement);
    }

    [HttpGet("geocode")]
    public async Task<IActionResult> Geocode([FromQuery] string name, CancellationToken cancellationToken)
    {
        JsonDocument result = await _openMeteoService.GeocodeAsync(name, cancellationToken);
        return Ok(result.RootElement);
    }
}