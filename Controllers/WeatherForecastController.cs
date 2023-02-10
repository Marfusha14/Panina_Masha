using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }
        [HttpPost]
        public IActionResult Add(string name)
        {

            Summaries.Add(name);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if(index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!");
            }
            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpGet("{index}")]
        public IActionResult GetAtIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!");
            }
            string name = Summaries[index];
            return Ok(name);
        }
        [HttpGet("name")]
        public IActionResult GetByName(string name)
        {
            string[] result = new string[Summaries.Count];
            for(int i = 0; i < Summaries.Count; i++)
            {
                if (Summaries[i].ToLower().Contains(name.ToLower()))
                {
                    result[i] = Summaries[i];
                }
            }
            result = result.Where(x => x != null).ToArray();
            return Ok(result);
        }
        [HttpGet("sortStrategy")]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (!sortStrategy.HasValue)
            {
                return Ok(Summaries);
            }
            else if(sortStrategy.Value == 1)
            {
                Summaries.Sort();
                return Ok(Summaries);
            }
            else if(sortStrategy.Value == -1)
            {
                Summaries.Sort();
                Summaries.Reverse();
                return Ok(Summaries);
            }
            else
            {
                return BadRequest("Некорректное значение параметра sortStrategy");
            }
        }

    }
       
}
