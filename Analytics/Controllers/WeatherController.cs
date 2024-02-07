using Analytics.Commands;
using Analytics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly DataContext _context;

        public WeatherController(DataContext context)
        {
            _context = context;
        }

        // GET: api/weather
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weather>>> GetWeatherData()
        {
            return await _context.Weather.OrderBy(w => w.Date).ToListAsync();
        }

        // GET: api/weather/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weather>> GetWeather(int id)
        {
            var weather = await _context.Weather.FindAsync(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        // POST: api/weather/import
        [HttpPost("import")]
        public async Task<ActionResult<Weather>> ImportWeather([FromServices] IImportWeatherCommand command, [FromForm] IFormFileCollection file)
        {
            if (file.Count == 0)
            {
                return NotFound("No file has been added for import.");
            }

            var result = await command.ExecuteAsync(file);

            return Ok(result);
        }
    }
}