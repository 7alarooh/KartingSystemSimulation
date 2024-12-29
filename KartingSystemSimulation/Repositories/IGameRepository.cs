using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IGameRepository
    {
        void Add(Game game);
        void Delete(Game game);
        IEnumerable<Game> GetAll();
        Game GetById(int gameId);
        void Update(Game game);
    }
}