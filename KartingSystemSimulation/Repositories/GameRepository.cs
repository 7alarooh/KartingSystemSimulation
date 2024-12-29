using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class GameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll() => _context.Games.ToList();
        public Game GetById(int gameId) => _context.Games.Find(gameId);
        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }
        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
        public void Delete(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }

}
