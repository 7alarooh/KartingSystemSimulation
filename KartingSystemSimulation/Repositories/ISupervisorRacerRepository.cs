using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRacerRepository
    {
        void Add(SupervisorRacer supervisorRacer);
        void Delete(SupervisorRacer supervisorRacer);
        IEnumerable<SupervisorRacer> GetAll();
        SupervisorRacer GetById(int supervisorId, int racerId);
    }
}