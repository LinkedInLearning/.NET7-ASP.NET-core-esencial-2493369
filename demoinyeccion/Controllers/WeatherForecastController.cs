using Microsoft.AspNetCore.Mvc;

namespace demoinyeccion.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public WeatherForecastController(IGuidinator guidinator)
    {

    }

    [HttpGet]
    public string Get(IGuidinator guidinator)
    {
        return guidinator.Value;
    }
}