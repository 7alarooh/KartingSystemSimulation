using Microsoft.AspNetCore.Mvc;
using KartingSystemSimulation.DTOs.GameDTOs;
using KartingSystemSimulation.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // Create a new game
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameInputDTO gameInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdGame = await _gameService.CreateGameAsync(gameInput);
            return CreatedAtAction(nameof(GetGameById), new { gameId = createdGame.GameId }, createdGame);
        }

        // Get all games
        [HttpGet]
        public async Task<ActionResult<List<GameOutputDTO>>> GetGames()
        {
            var games = await _gameService.GetGamesAsync();
            return Ok(games);
        }

        // Get a game by ID
        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameOutputDTO>> GetGameById(int gameId)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(gameId);
                return Ok(game);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Update a game with top racers and live race updates
        [HttpPut("{gameId}")]
        public async Task<IActionResult> UpdateGame(int gameId, [FromBody] UpdateGameDTO updateGameDto)
        {
            try
            {
                await _gameService.UpdateGameAsync(gameId, updateGameDto.TopRacers, updateGameDto.LiveRaceUpdates);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Delete a game
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            try
            {
                await _gameService.DeleteGameAsync(gameId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

    // DTO for updating a game
    public class UpdateGameDTO
    {
        public List<int> TopRacers { get; set; }
        public string LiveRaceUpdates { get; set; }
    }
}
