using KartingSystemSimulation.DTOs.GameDTOs;

namespace KartingSystemSimulation.Services
{
    public interface IGameService
    {
        Task<GameOutputDTO> CreateGameAsync(GameInputDTO gameInput);
        Task DeleteGameAsync(int gameId);
        Task<GameOutputDTO> GetGameByIdAsync(int gameId);
        Task<List<GameOutputDTO>> GetGamesAsync();
        Task UpdateGameAsync(int gameId, List<int> topRacers, string liveRaceUpdates);
    }
}