using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRacerRepository
    {
        // Add a new Racer record to the database
        void Add(Racer racer);

        // Delete an existing Racer record from the database
        void Delete(Racer racer);

        // Retrieve all Racer records from the database
        IEnumerable<Racer> GetAll();

        // Retrieve a specific Racer record by its unique ID
        Racer GetById(int racerId);

        // Update an existing Racer record in the database
        void Update(Racer racer);
    }
}
