using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RacerController : ControllerBase
    {
        private readonly IRacerService _racerService;

        public RacerController(IRacerService racerService)
        {
            _racerService = racerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Racer>> GetAll()
        {
            return Ok(_racerService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Racer> GetById(int id)
        {
            var racer = _racerService.GetById(id);
            if (racer == null)
            {
                return NotFound();
            }
            return Ok(racer);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Racer racer)
        {
            try
            {
                _racerService.Add(racer);
                return CreatedAtAction(nameof(GetById), new { id = racer.RacerId }, racer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Racer racer)
        {
            if (id != racer.RacerId)
            {
                return BadRequest("Racer ID mismatch.");
            }

            try
            {
                _racerService.Update(racer);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _racerService.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
