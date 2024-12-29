using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IUserRepository
    {
        // Add a new User record to the database
        void Add(User user);

        // Delete an existing User record from the database
        void Delete(User user);

        // Retrieve all User records from the database
        IEnumerable<User> GetAll();

        // Retrieve a specific User record by its unique ID
        User GetById(int userId);

        // Update an existing User record in the database
        void Update(User user);
    }
}
