using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILeaderboardRepository
    {
        void AddLeaderboard(Leaderboard leaderboard);
        void DeleteLeaderboard(Leaderboard leaderboard);
        IEnumerable<Leaderboard> GetAllLeaderboards();
        Leaderboard GetLeaderboardById(int leaderboardId);
        IEnumerable<Leaderboard> GetLeaderboardByPeriod(Period period);
        void UpdateLeaderboard(Leaderboard leaderboard);
    }
}