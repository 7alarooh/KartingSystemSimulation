using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceHistoryRepository
    {
        void Add(RaceHistory history);
        void Delete(RaceHistory history);
        IEnumerable<RaceHistory> GetAll();
        RaceHistory GetById(int historyId);
        void Update(RaceHistory history);
    }
}