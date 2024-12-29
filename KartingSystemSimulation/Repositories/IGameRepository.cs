using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        void DeleteGame(Game game);
        IEnumerable<Game> GetAllGames();
        Game GetGameById(int gameId);
        void UpdateGame(Game game);
    }
}