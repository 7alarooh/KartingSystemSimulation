using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILiveRaceRepository
    {
        void Add(LiveRace liveRace);
        void Delete(LiveRace liveRace);
        IEnumerable<LiveRace> GetAll();
        LiveRace GetById(int raceId);
        void Update(LiveRace liveRace);
    }
}