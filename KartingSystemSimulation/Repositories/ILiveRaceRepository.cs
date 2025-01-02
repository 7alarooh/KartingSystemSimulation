using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    using KartingSystemSimulation.Models;
    using System.Collections.Generic;

    public interface ILiveRaceRepository
    {
        IEnumerable<LiveRace> GetAllLiveRaces();
        LiveRace GetLiveRaceById(int liveRaceId);
        void AddLiveRace(LiveRace liveRace);
        void UpdateLiveRace(LiveRace liveRace);
        void DeleteLiveRace(LiveRace liveRace);
        LiveRace GetByIdWithRacers(int liveRaceId); // Includes racers
    }

}