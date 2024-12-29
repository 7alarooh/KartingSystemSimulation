using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRacerRepository
    {
        // Add a new SupervisorRacer record to the database
        void Add(SupervisorRacer supervisorRacer);

        // Delete an existing SupervisorRacer record from the database
        void Delete(SupervisorRacer supervisorRacer);

        // Retrieve all SupervisorRacer records from the database
        IEnumerable<SupervisorRacer> GetAll();

        // Retrieve a specific SupervisorRacer record by the composite key (SupervisorId and RacerId)
        SupervisorRacer GetById(int supervisorId, int racerId);
    }
}
