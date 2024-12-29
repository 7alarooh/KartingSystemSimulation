using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceHistoryLeaderboardRepository : IRaceHistoryLeaderboardRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceHistoryLeaderboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceHistory> GetAllRaceHistories() => _context.RaceHistories.ToList();
        public IEnumerable<Leaderboard> GetAllLeaderboards() => _context.Leaderboards.ToList();
    }

}
