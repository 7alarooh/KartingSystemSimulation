using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceHistoryRepository : IRaceHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceHistory> GetAllRaceHistories() => _context.RaceHistories.ToList();
        public RaceHistory GetRaceHistoryById(int historyId) => _context.RaceHistories.Find(historyId);
        public void AddRaceHistory(RaceHistory history)// Adds a new RaceHistory entity to the database
        {
            _context.RaceHistories.Add(history);
            _context.SaveChanges();
        }
        public void UpdateRaceHistory(RaceHistory history)// Updates The RaceHistory Table
        {
            _context.RaceHistories.Update(history);
            _context.SaveChanges();
        }
        public void DeleteRaceHistory(RaceHistory history)//Deletes an existing History from datbase
        {
            _context.RaceHistories.Remove(history);
            _context.SaveChanges();
        }
    }

}
