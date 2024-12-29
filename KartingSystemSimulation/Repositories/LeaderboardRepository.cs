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
        

        public IEnumerable<Leaderboard> GetAll() => _context.Leaderboards.ToList();
        public Leaderboard GetById(int leaderboardId) => _context.Leaderboards.Find(leaderboardId);
        public void Add(Leaderboard leaderboard)
        {
            _context.Leaderboards.Add(leaderboard);// Adds a new Leaderboard entity to the database
            _context.SaveChanges();
        }
        public void Update(Leaderboard leaderboard)// Updates an existing Leaderboard entity in the database
        {
            _context.Leaderboards.Update(leaderboard);
            _context.SaveChanges();
        }
        public void Delete(Leaderboard leaderboard)// Deletes a Leaderboard entity from the database
        {
            _context.Leaderboards.Remove(leaderboard);
            _context.SaveChanges();
        }
    }

}
