using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Services
{
    public interface IRacerService
    {
        void AddRacer(Racer racer, int currentUserId);
        void DeleteRacer(int racerId, int currentUserId);
        void DeleteRacer(int racerId, string role);
        IEnumerable<Racer> GetAllRacers();
        Racer GetRacerById(int racerId);
        void UpdateRacer(Racer racer);
    }
}