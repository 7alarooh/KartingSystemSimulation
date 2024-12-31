using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // Create a new game
        public async Task<GameOutputDTO> CreateGameAsync(GameInputDTO gameInput)
        {
            // Map input DTO to the Game model
            var game = new Game
            {
                RaceType = Enum.TryParse(gameInput.RaceType, out RaceType raceType) ? raceType : RaceType.Unknown,
                Laps = gameInput.Laps,
                RaceDate = gameInput.RaceDate,
                KartId = gameInput.KartId,
                TopRacers = string.Empty, // Initial placeholder for top racers, can be updated later
                LiveRaceUpdates = new List<LiveRace>(), // Initially, no live race updates
            };

            // Add the game to the repository (database)
            _gameRepository.AddGame(game);

            // Return the output DTO for the newly created game
            return new GameOutputDTO
            {
                GameId = game.GameId,
                RaceType = game.RaceType.ToString(),
                Laps = game.Laps,
                RaceDate = game.RaceDate,
                KartId = game.KartId,
                RacerNames = new List<string>(), // Initially empty, will be filled later
                TopRacers = new List<int>() // Initially empty, will be updated later
            };
        }

        // Get all games
        public async Task<List<GameOutputDTO>> GetGamesAsync()
        {
            var games = _gameRepository.GetAllGames();

            // Map the games to output DTOs
            var gameOutputDTOs = games.Select(game => new GameOutputDTO
            {
                GameId = game.GameId,
                RaceType = game.RaceType.ToString(),
                Laps = game.Laps,
                RaceDate = game.RaceDate,
                KartId = game.KartId,
                RacerNames = new List<string>(), // Can be populated based on game participants
                TopRacers = string.IsNullOrEmpty(game.TopRacers) ? new List<int>() : game.TopRacers.Split(',').Select(int.Parse).ToList()
            }).ToList();

            return gameOutputDTOs;
        }

        // Get a game by ID
        public async Task<GameOutputDTO> GetGameByIdAsync(int gameId)
        {
            var game = _gameRepository.GetGameById(gameId);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");
            }

            // Map to the output DTO
            return new GameOutputDTO
            {
                GameId = game.GameId,
                RaceType = game.RaceType.ToString(),
                Laps = game.Laps,
                RaceDate = game.RaceDate,
                KartId = game.KartId,
                RacerNames = new List<string>(), // Could be populated with racer names later
                TopRacers = string.IsNullOrEmpty(game.TopRacers) ? new List<int>() : game.TopRacers.Split(',').Select(int.Parse).ToList()
            };
        }

        // Update a game with top racers and live race updates
        public async Task UpdateGameAsync(int gameId, List<int> topRacers, string liveRaceUpdates)
        {
            var game = _gameRepository.GetGameById(gameId);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");
            }

            // Update top racers and live race updates
            game.TopRacers = string.Join(",", topRacers); // Convert list of top racers to a comma-separated string
            game.LiveRaceUpdates = new List<LiveRace> { new LiveRace { UpdateDetails = liveRaceUpdates, RaceDate = DateTime.Now } };

            // Update the game in the repository
            _gameRepository.UpdateGame(game);
        }

        // Delete a game
        public async Task DeleteGameAsync(int gameId)
        {
            var game = _gameRepository.GetGameById(gameId);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} not found.");
            }

            // Delete the game from the repository
            _gameRepository.DeleteGame(game);
        }
    }
}
