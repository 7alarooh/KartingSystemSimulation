using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly ApplicationDbContext _context;// Database context for accessing the database


        public LeaderboardRepository(ApplicationDbContext context)// Constructor to initialize the repository with the database context
        {
            _context = context;
        }


        public IEnumerable<Leaderboard> GetAllLeaderboards() => _context.Leaderboards.ToList();
        public Leaderboard GetLeaderboardById(int leaderboardId) => _context.Leaderboards.Find(leaderboardId);
        public void AddLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboards.Add(leaderboard);// Adds a new Leaderboard entity to the database
            _context.SaveChanges();
        }
        public void UpdateLeaderboard(Leaderboard leaderboard)// Updates an existing Leaderboard entity in the database
        {
            _context.Leaderboards.Update(leaderboard);
            _context.SaveChanges();
        }
        public void DeleteLeaderboard(Leaderboard leaderboard)// Deletes a Leaderboard entity from the database
        {
            _context.Leaderboards.Remove(leaderboard);
            _context.SaveChanges();
        }
    }

}
