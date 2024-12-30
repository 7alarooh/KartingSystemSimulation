using KartingSystemSimulation.DTOs;
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
        public ActionResult<IEnumerable<Racer>> GetAllRacer()
        {
            return Ok(_racerService.GetAllRacers());
        }

        [HttpGet("{id}")]
        public ActionResult<Racer> GetRacerById(int id)
        {
            var racer = _racerService.GetRacerById(id);
            if (racer == null)
            {
                return NotFound();
            }
            return Ok(racer);
        }

        [HttpPost]
        public IActionResult AddRacer([FromBody] RacerInputDTO racerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // You can replace 1 with a dynamic value from the logged-in user
                _racerService.AddRacer(racerDto, currentUserId: 1);
                return Ok("Racer added successfully.");
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
                _racerService.UpdateRacer(racer);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRacer(int id, string role)
        {
            try
            {
                _racerService.DeleteRacer(id, role);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
