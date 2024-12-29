using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Services
{
    public interface IRacerService
    {
        void AddRacer(Racer racer, int currentUserId);
        void DeleteRacer(int racerId, int currentUserId);
        void DeleteRacer(int racerId, string role);
        IEnumerable<Racer> GetAll();
        Racer GetById(int racerId);
        void Update(Racer racer);
    }
}