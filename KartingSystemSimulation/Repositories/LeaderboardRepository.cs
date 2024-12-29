using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Leaderboard> GetAll() => _context.Leaderboards.ToList();
        public Leaderboard GetById(int leaderboardId) => _context.Leaderboards.Find(leaderboardId);
        public void Add(Leaderboard leaderboard)
        {
            _context.Leaderboards.Add(leaderboard);
            _context.SaveChanges();
        }
        public void Update(Leaderboard leaderboard)
        {
            _context.Leaderboards.Update(leaderboard);
            _context.SaveChanges();
        }
        public void Delete(Leaderboard leaderboard)
        {
            _context.Leaderboards.Remove(leaderboard);
            _context.SaveChanges();
        }
    }

}
