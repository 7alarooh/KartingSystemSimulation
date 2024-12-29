using KartingSystemSimulation.Models; // Provides access to the Admin model

namespace KartingSystemSimulation.Repositories
{
    public class AdminRepository // Handles CRUD operations for Admin entities
    {
        private readonly ApplicationDbContext _context; // EF Core database context

        public AdminRepository(ApplicationDbContext context) // Constructor injecting the DbContext
        {
            _context = context; // Assigns the passed context to the local field
        }

        public IEnumerable<Admin> GetAll() => _context.Admins.ToList(); // Retrieves all Admin records

        public Admin GetById(int adminId) => _context.Admins.Find(adminId); // Retrieves a single Admin by its ID

        public void Add(Admin admin) // Adds a new Admin record
        {
            _context.Admins.Add(admin); // Attaches the new Admin entity to the context
            _context.SaveChanges();     // Commits the changes to the database
        }

        public void Update(Admin admin) // Updates an existing Admin record
        {
            _context.Admins.Update(admin); // Marks the Admin entity as modified
            _context.SaveChanges();        // Commits the updated data to the database
        }

        public void Delete(Admin admin) // Deletes an Admin record
        {
            _context.Admins.Remove(admin); // Removes the Admin entity from the context
            _context.SaveChanges();        // Saves changes, finalizing the deletion
        }
    }
}
