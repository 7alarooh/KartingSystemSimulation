using KartingSystemSimulation.DTOs.LeaderboardDTO_s;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService; // Inject the leaderboard service
        }

        // Add a racer to the leaderboard
        [HttpPost("AddToLeaderboard")]
        public IActionResult AddToLeaderboard([FromBody] LeaderboardInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Validate the input

            try
            {
                var result = _leaderboardService.AddToLeaderboard(input);
                return Ok(result); // Return the added leaderboard entry
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message }); // Handle racer not found
            }
        }

        // Get leaderboard for a specific period
        [HttpGet("GetLeaderboard")]
        public IActionResult GetLeaderboard([FromQuery] Period period)
        {
            try
            {
                var leaderboard = _leaderboardService.GetLeaderboard(period);
                return Ok(leaderboard); // Return the leaderboard for the specified period
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message }); // Handle period not found
            }
        }

        // Update a leaderboard entry
        [HttpPut("UpdateLeaderboard/{id}")]
        public IActionResult UpdateLeaderboard(int id, [FromBody] LeaderboardInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Validate the input

            try
            {
                _leaderboardService.UpdateLeaderboard(id, input);
                return Ok(new { Message = "Leaderboard entry updated successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message }); // Handle leaderboard entry not found
            }
        }

        // Delete a leaderboard entry
        [HttpDelete("DeleteLeaderboardEntry/{id}")]
        public IActionResult DeleteLeaderboardEntry(int id)
        {
            try
            {
                _leaderboardService.DeleteLeaderboardEntry(id);
                return Ok(new { Message = "Leaderboard entry deleted successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message }); // Handle leaderboard entry not found
            }
        }
    }
}
