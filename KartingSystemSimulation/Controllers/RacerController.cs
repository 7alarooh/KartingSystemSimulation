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
        private readonly HttpClient _httpClient;
        private readonly string _supervisorApiBaseUrl = "https://localhost:7087/api/supervisor";

        public RacerController(IRacerService racerService, HttpClient httpClient)
        {
            _racerService = racerService; // Initialize racer service
            _httpClient = httpClient;
        }

        // Add a new racer
        [HttpPost]
        public async Task<IActionResult> AddRacer([FromBody] RacerInputDTO racerInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors.
            }
            var age = _racerService.CalculateAge(racerInput.DOB);

            if (age < 18)
            {
                var response = await _httpClient.PostAsJsonAsync(_supervisorApiBaseUrl, racerInput.SupervisorId);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Failed to add supervisor. Please check the details.");
                }

                // Get the created supervisor ID
                var supervisorId = await response.Content.ReadAsStringAsync();
                racerInput.SupervisorId = int.Parse(supervisorId); // Assign the supervisor ID to the racer
            }

            try
            {
                _racerService.AddRacer(racerInput);
                return Ok("Racer added successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle supervisor validation error
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
