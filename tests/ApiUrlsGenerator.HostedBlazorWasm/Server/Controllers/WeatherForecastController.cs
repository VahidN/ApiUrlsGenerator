using ApiUrlsGenerator.HostedBlazorWasm.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApiUrlsGenerator.HostedBlazorWasm.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) => _logger = logger;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                                                      {
                                                          Date = DateTime.Now.AddDays(index),
                                                          TemperatureC = Random.Shared.Next(-20, 55),
                                                          Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                                                      })
                         .ToArray();
    }

    [HttpGet("_secretUrl")]
    [HttpPost("NotSecretUrl")]
    public IActionResult SecretUrl() => Ok();

    public ActionResult<byte[]> GetBytes() => new ActionResult<byte[]>(new byte[1]);

    [HttpGet("[action]/{id:int}")]
    public WeatherForecast GetSingle(int id) => new WeatherForecast();
}