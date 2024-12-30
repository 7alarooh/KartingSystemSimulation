using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Services
{
    public interface ISupervisorService
    {
        void AddSupervisor(Supervisor supervisor);
        void DeleteSupervisor(int supervisorId);
        IEnumerable<Supervisor> GetAllSupervisors();
        Supervisor GetSupervisorById(int supervisorId);
        void UpdateSupervisor(Supervisor supervisor);
    }
}