using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceRacerRepository
    {
        void Add(RaceRacer raceRacer);
        void Delete(RaceRacer raceRacer);
        IEnumerable<RaceRacer> GetAll();
        RaceRacer GetById(int raceId, int racerId);
    }
}