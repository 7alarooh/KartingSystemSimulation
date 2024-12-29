using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRepository
    {
        // Add a new Supervisor record to the database
        void Add(Supervisor supervisor);

        // Delete an existing Supervisor record from the database
        void Delete(Supervisor supervisor);

        // Retrieve all Supervisor records from the database
        IEnumerable<Supervisor> GetAll();

        // Retrieve a specific Supervisor record by its unique ID
        Supervisor GetById(int supervisorId);

        // Update an existing Supervisor record in the database
        void Update(Supervisor supervisor);
    }
}
