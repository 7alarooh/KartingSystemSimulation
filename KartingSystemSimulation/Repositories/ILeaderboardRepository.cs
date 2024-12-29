using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILeaderboardRepository
    {
        void Add(Leaderboard leaderboard);
        void Delete(Leaderboard leaderboard);
        IEnumerable<Leaderboard> GetAll();
        Leaderboard GetById(int leaderboardId);
        void Update(Leaderboard leaderboard);
    }
}