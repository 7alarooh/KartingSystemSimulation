using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IAdminRepository
    {
        // Add a new Admin record to the database
        void Add(Admin admin);

        // Delete an existing Admin record from the database
        void Delete(Admin admin);

        // Retrieve all Admin records from the database
        IEnumerable<Admin> GetAll();

        // Retrieve a specific Admin record by its unique ID
        Admin GetById(int adminId);

        // Update an existing Admin record in the database
        void Update(Admin admin);
    }
}
