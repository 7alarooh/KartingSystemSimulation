using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRepository1
    {
        Supervisor AddSupervisor(Supervisor supervisor);
        void DeleteSupervisor(Supervisor supervisor);
        IEnumerable<Supervisor> GetAllSupervisors();
        Supervisor GetSupervisorByEmail(string SupervisorEmail);
        Supervisor GetSupervisorById(int supervisorId);
        void UpdateSupervisor(Supervisor supervisor);
    }
}