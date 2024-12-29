using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;// Database context for accessing the database

        public GameRepository(ApplicationDbContext context)// Constructor to initialize the repository with the database context
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll() => _context.Games.ToList();// Retrieves all Game entities from the database
        public Game GetById(int gameId) => _context.Games.Find(gameId);// Retrieves a specific Game entity by its ID
        public void Add(Game game)// Adds a new Game entity to the database
        {
            _context.Games.Add(game);// Adds the game to the context
            _context.SaveChanges();// Saves changes to the database
        }
        public void Update(Game game)// Updates an existing Game entity in the database
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
        public void Delete(Game game)// Deletes a Game entity from the database
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }

}
