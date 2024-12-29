using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceHistoryLeaderboardRepository
    {
        // Retrieve all Leaderboard records linked to RaceHistories
        IEnumerable<Leaderboard> GetAllLeaderboards();

        // Retrieve all RaceHistory records linked to Leaderboards
        IEnumerable<RaceHistory> GetAllRaceHistories();
    }
}
