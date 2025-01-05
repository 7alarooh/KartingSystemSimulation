using KartingSystemSimulation.DTOs.GameDTOs;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KartingSystemSimulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        // Constructor to initialize the GameService dependency
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // POST: api/Game
        [HttpPost]
        public async Task<ActionResult<GameOutputDTO>> CreateGame([FromBody] GameInputDTO gameInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call the service to create a game and return the output DTO
            var createdGame = await _gameService.CreateGameAsync(gameInput);

            // Return the created game with status 201
            return CreatedAtAction(nameof(GetGameById), new { id = createdGame.GameId }, createdGame);
        }

        // GET: api/Game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameOutputDTO>>> GetGames()
        {
            // Call the service to get all games
            var games = await _gameService.GetGamesAsync();

            // Return the list of games
            return Ok(games);
        }

        // GET: api/Game/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GameOutputDTO>> GetGameById(int id)
        {
            try
            {
                // Call the service to get a game by ID
                var game = await _gameService.GetGameByIdAsync(id);

                // Return the game details
                return Ok(game);
            }
            catch (KeyNotFoundException)
            {
                // If game is not found, return 404
                return NotFound();
            }
        }

        // PUT: api/Game/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] GameInputDTO gameUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Call the service to update the game
                await _gameService.UpdateGameAsync(id, gameUpdate.TopRacers, gameUpdate.LiveRaceUpdates);

                // Return 204 No Content if the update is successful
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                // If game is not found, return 404
                return NotFound();
            }
        }

        // DELETE: api/Game/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                // Call the service to delete the game
                await _gameService.DeleteGameAsync(id);

                // Return 204 No Content if the deletion is successful
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                // If game is not found, return 404
                return NotFound();
            }
        }
    }
}
