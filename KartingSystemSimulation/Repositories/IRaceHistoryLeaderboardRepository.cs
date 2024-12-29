using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceHistoryLeaderboardRepository
    {
        IEnumerable<Leaderboard> GetAllLeaderboards();
        IEnumerable<RaceHistory> GetAllRaceHistories();
    }
}