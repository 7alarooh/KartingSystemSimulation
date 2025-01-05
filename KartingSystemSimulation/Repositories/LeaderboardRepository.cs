using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{


    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly ApplicationDbContext _context; // Database context

        public LeaderboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Leaderboard> GetAllLeaderboards() => _context.Leaderboards.ToList();

        public Leaderboard GetLeaderboardById(int leaderboardId) => _context.Leaderboards.Find(leaderboardId);

        // Fetch leaderboard entries by period
        public IEnumerable<Leaderboard> GetLeaderboardByPeriod(Period period)
        {
            return _context.Leaderboards
                .Where(l => l.Period == period)
                .ToList();
        }

        public void AddLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboards.Add(leaderboard);
            _context.SaveChanges();
        }

        public void UpdateLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboards.Update(leaderboard);
            _context.SaveChanges();
        }

        public void DeleteLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboards.Remove(leaderboard);
            _context.SaveChanges();
        }
    }


}
