using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILeaderboardRepository
    {
        void AddLeaderboard(Leaderboard leaderboard);
        void DeleteLeaderboard(Leaderboard leaderboard);
        IEnumerable<Leaderboard> GetAllLeaderboards();
        Leaderboard GetLeaderboardById(int leaderboardId);
        void UpdateLeaderboard(Leaderboard leaderboard);
    }
}