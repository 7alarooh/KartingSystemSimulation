using KartingSystemSimulation.Models; // Provides access to the Admin model

namespace KartingSystemSimulation.Repositories
{
    public class AdminRepository : IAdminRepository

    // Handles CRUD operations for Admin entities
    {
        private readonly ApplicationDbContext _context; // EF Core database context

        public AdminRepository(ApplicationDbContext context) // Constructor injecting the DbContext
        {
            _context = context; // Assigns the passed context to the local field
        }

        public IEnumerable<Admin> GetAllAdmins() => _context.Admins.ToList(); // Retrieves all Admin records

        public Admin GetAdminById(int adminId) => _context.Admins.Find(adminId); // Retrieves a single Admin by its ID

        public void AddAdmin(Admin admin) // Adds a new Admin record
        {
            _context.Admins.Add(admin); // Attaches the new Admin entity to the context
            _context.SaveChanges();     // Commits the changes to the database
        }

        public void UpdateAdmin(Admin admin) // Updates an existing Admin record
        {
            _context.Admins.Update(admin); // Marks the Admin entity as modified
            _context.SaveChanges();        // Commits the updated data to the database
        }

        public void DeleteAdmin(Admin admin) // Deletes an Admin record
        {
            _context.Admins.Remove(admin); // Removes the Admin entity from the context
            _context.SaveChanges();        // Saves changes, finalizing the deletion
        }
    }
}
