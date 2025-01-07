using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ISupervisorRacerRepository
    {
        void AddSupervisorRacer(SupervisorRacer supervisorRacer);
        void DeleteSupervisorRacer(SupervisorRacer supervisorRacer);
        IEnumerable<SupervisorRacer> GetAllSupervisorRacers();
        SupervisorRacer GetSupervisorRacerById(int supervisorId, int racerId);
       
        
    }
}