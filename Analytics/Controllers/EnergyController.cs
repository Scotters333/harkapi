using Analytics.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Analytics.Models;

namespace Analytics.Controllers
{
    [Route("api/energy")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly DataContext _context;

        public EnergyController(DataContext context)
        {
            _context = context;
        }

        // GET: api/energy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Energy>>> GetEnergyData()
        {
            return await _context.Energy.ToListAsync();
        }

        // GET: api/energy
        [HttpGet("anomolies")]
        public async Task<ActionResult<IEnumerable<Energy>>> GetAnomolies()
        {
            return await _context.Energy.Where(e => e.IsAnomaly).ToListAsync();
        }

        // GET: api/energy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Energy>> GetEnergy(int id)
        {
            var energy = await _context.Energy.FindAsync(id);

            if (energy == null)
            {
                return NotFound();
            }

            return energy;
        }

        // POST: api/energy/import
        [HttpPost("import")]
        public async Task<ActionResult<Energy>> ImportEnergy([FromServices] IImportEnergyCommand command, [FromForm] IFormFileCollection file)
        {
            if (file.Count == 0)
            {
                return NotFound("No file has been added for import.");
            }

            var result = await command.ExecuteAsync(file);

            return Ok(result);
        }

        // POST: api/energy/importanomolies
        [HttpPost("importanomolies")]
        public async Task<ActionResult<Energy>> ImportAnomolies([FromServices] IUpdateEnergyAnomoliesCommand command, [FromForm] IFormFileCollection file)
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
