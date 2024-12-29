using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; // Database context for accessing the database

        public UserRepository(ApplicationDbContext context) // Constructor to initialize the repository with the database context
        {
            _context = context;
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList(); // Retrieves all User entities from the database

        public User GetById(int userId) => _context.Users.Find(userId); // Retrieves a specific User entity by its ID

        public void Add(User user) // Adds a new User entity to the database
        {
            _context.Users.Add(user); // Adds the user to the context
            _context.SaveChanges(); // Saves changes to the database
        }

        public void Update(User user) // Updates an existing User entity in the database
        {
            _context.Users.Update(user); // Updates the user in the context
            _context.SaveChanges(); // Saves changes to the database
        }

        public void Delete(User user) // Deletes a User entity from the database
        {
            _context.Users.Remove(user); // Removes the user from the context
            _context.SaveChanges(); // Saves changes to the database
        }
    }
}
