using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceHistoryRepository
    {
        void AddRaceHistory(RaceHistory history);
        void DeleteRaceHistory(RaceHistory history);
        IEnumerable<RaceHistory> GetAllRaceHistories();
        RaceHistory GetRaceHistoryById(int historyId);
        void UpdateRaceHistory(RaceHistory history);
    }
}