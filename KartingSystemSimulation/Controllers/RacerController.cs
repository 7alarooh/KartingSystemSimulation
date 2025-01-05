using Azure;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;


namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/racers")]
    public class RacerController : ControllerBase
    {
        private readonly IRacerService _racerService;
        private readonly IEmailService _emailService;
        private readonly IMembershipService _membershipService;
        public RacerController(IRacerService racerService, IEmailService emailService, IMembershipService membershipService)
        {
            _racerService = racerService; // Initialize racer service
            _emailService = emailService;
            _membershipService = membershipService;
        }

        // Add a new racer
        [HttpPost]
        public async Task<IActionResult> AddRacer([FromBody] RacerInputDTO racerInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors.
            }

            try
            {
                // Call the service to add the racer
                _racerService.AddRacer(racerInput);
                return Ok("Racer added successfully and notification email sent."); // Success response
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { ErrorCode = "DuplicateEntry", ErrorMessage = ex.Message }); // Conflict error
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message }); // Generic server error
            }
        }


        // Get racer by ID
        [HttpGet("{id}")]
        public IActionResult GetRacerById(int id)
        {
            try
            {
                var racer = _racerService.GetRacerById(id);
                return Ok(racer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Get all racers
        [HttpGet]
        public IActionResult GetAllRacers()
        {
            var racers = _racerService.GetAllRacers();
            return Ok(racers);
        }

        // Update racer
        [HttpPut("{id}")]
        public IActionResult UpdateRacer(int id, [FromBody] RacerInputDTO racerInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                _racerService.UpdateRacer(id, racerInput);
                return Ok("Racer updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Delete racer
        [HttpDelete("{id}")]
        public IActionResult DeleteRacer(int id)
        {
            try
            {
                _racerService.DeleteRacer(id);
                return Ok("Racer deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
