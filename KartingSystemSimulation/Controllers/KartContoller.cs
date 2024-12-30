using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/karts")]
    public class KartController : ControllerBase
    {
        private readonly IKartService _kartService; // Service to handle kart logic

        public KartController(IKartService kartService)
        {
            _kartService = kartService; // Initialize the kart service
        }

        // Add a new kart
        [HttpPost]
        public IActionResult AddKart([FromBody] KartInputDTO kartInput)
        {
            if (!ModelState.IsValid) // Check for validation errors
            {
                return BadRequest(ModelState); // Return validation errors to the client
            }

            _kartService.AddKart(kartInput); // Call the service to add a new kart
            return Ok("Kart added successfully."); // Return success message
        }

        // Get kart by ID
        [HttpGet("{id}")]
        public IActionResult GetKartById(int id)
        {
            try
            {
                var kart = _kartService.GetKartById(id); // Fetch kart details
                return Ok(kart); // Return kart details
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Handle missing kart
            }
        }

        // Get all karts
        [HttpGet]
        public IActionResult GetAllKarts()
        {
            var karts = _kartService.GetAllKarts(); // Fetch all karts
            return Ok(karts); // Return list of karts
        }

        // Update kart details
        [HttpPut("{id}")]
        public IActionResult UpdateKart(int id, [FromBody] KartInputDTO kartInput)
        {
            if (!ModelState.IsValid) // Check for validation errors
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                _kartService.UpdateKart(id, kartInput); // Call the service to update kart
                return Ok("Kart updated successfully."); // Return success message
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Handle missing kart
            }
        }

        // Delete kart
        [HttpDelete("{id}")]
        public IActionResult DeleteKart(int id)
        {
            try
            {
                _kartService.DeleteKart(id); // Call the service to delete kart
                return Ok("Kart deleted successfully."); // Return success message
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Handle missing kart
            }
        }
    }
}
