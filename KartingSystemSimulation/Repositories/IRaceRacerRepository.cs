using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceRacerRepository
    {
        void AddRaceRacer(RaceRacer raceRacer);
        void DeleteRaceRacer(RaceRacer raceRacer);
        IEnumerable<RaceRacer> GetAllRaceRacers();
        RaceRacer GetRaceRacerById(int raceId, int racerId);
    }
}