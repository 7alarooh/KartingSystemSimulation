using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceHistoryLeaderboardRepository : IRaceHistoryLeaderboardRepository
    {
        private readonly ApplicationDbContext _context;// Database context for accessing the database
        public RaceHistoryLeaderboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceHistory> GetAllRaceHistories() => _context.RaceHistories.ToList();// Retrieves all RaceHistory entities from the database
        public IEnumerable<Leaderboard> GetAllLeaderboards() => _context.Leaderboards.ToList();// Retrieves all Leaderboard entities from the database
    }

}
