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
        public IActionResult AddRacer([FromBody] Racer racer)
        {
            try
            {
                // Extract the current user's ID from the JWT token
                var currentUserId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);

                // Call the service method with the current user's ID
                _racerService.AddRacer(racer, currentUserId);

                return CreatedAtAction(nameof(GetRacerById), new { id = racer.RacerId }, racer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Return validation errors
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Return unexpected errors
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
