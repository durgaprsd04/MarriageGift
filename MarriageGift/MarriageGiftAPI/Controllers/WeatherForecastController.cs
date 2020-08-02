using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace MarriageGiftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static List<WeatherForecast> listOfItems = new List<WeatherForecast>();
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            for(int i=0;i<5;i++)
            {
              var v = new WeatherForecast
              {
                  Date = DateTime.Now.AddDays(i),
                  TemperatureC = rng.Next(-20, 55),
                  Summary = Summaries[rng.Next(Summaries.Length)]
              };
              listOfItems.Add(v);
            }
              return listOfItems;

        }
      [HttpPost]
      public  ActionResult<WeatherForecast> PostTodoItem(WeatherForecast weatherForecast)
      {
        listOfItems.Clear();
        var newW = new WeatherForecast { Date = weatherForecast.Date, TemperatureC = weatherForecast.TemperatureC, Summary=weatherForecast.Summary };
            listOfItems.Add(newW);
            return CreatedAtAction("Get",newW , weatherForecast);
      }

    }
}
