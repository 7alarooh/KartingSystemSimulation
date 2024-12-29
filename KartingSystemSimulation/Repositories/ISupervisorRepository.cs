using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRepository
    {
        void AddSupervisor(Supervisor supervisor);
        void DeleteSupervisor(Supervisor supervisor);
        IEnumerable<Supervisor> GetAllSupervisors();
        Supervisor GetSupervisorById(int supervisorId);
        void UpdateSupervisor(Supervisor supervisor);
    }
}