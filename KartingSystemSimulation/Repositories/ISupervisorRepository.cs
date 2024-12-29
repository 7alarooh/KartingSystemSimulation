using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRepository
    {
        void Add(Supervisor supervisor);
        void Delete(Supervisor supervisor);
        IEnumerable<Supervisor> GetAll();
        Supervisor GetById(int supervisorId);
        void Update(Supervisor supervisor);
    }
}