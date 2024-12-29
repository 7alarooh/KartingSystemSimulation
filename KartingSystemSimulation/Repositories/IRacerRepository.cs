using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRacerRepository
    {
        void AddRacer(Racer racer);
        void DeleteRacer(Racer racer);
        IEnumerable<Racer> GetAllRacers();
        Racer GetRacerById(int racerId);
        void UpdateRacer(Racer racer);
    }
}