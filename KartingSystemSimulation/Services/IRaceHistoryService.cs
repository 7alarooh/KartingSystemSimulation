using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Services
{
    public interface IRaceHistoryService
    {
        void AddRacerHistoryFromLiveRace(int liveRaceId);
        IEnumerable<RaceHistory> GetAllRaceHistories();
        RaceHistory GetRaceHistoryById(int historyId);
    }
}