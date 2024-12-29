using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILiveRaceRepository
    {
        void AddLiveRaces(LiveRace liveRace);
        void DeleteLiveRace(LiveRace liveRace);
        IEnumerable<LiveRace> GetAllLiveRaces();
        LiveRace GetLiveRaceById(int raceId);
        void UpdateLiveRace(LiveRace liveRace);
    }
}