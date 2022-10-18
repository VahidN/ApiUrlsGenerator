using ApiUrlsGenerator.HostedBlazorWasm.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApiUrlsGenerator.HostedBlazorWasm.Server.Areas.Test.Controllers;

[ApiController]
[Route("api/Test/[controller]")]
[Area("Test")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) => _logger = logger;

    [HttpGet(Name = "GetAreaWeatherForecast")]
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
    public IActionResult SecretUrl() => Ok();

    public ActionResult<byte[]> GetBytes() => new ActionResult<byte[]>(new byte[1]);

    public WeatherForecast GetSingle(int id) =>
        //var test = ApiUrl.WeatherForecast.SecretUrl;
        new WeatherForecast();
}