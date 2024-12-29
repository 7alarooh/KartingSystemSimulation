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

        public IEnumerable<RaceHistory> GetAll() => _context.RaceHistories.ToList();
        public RaceHistory GetById(int historyId) => _context.RaceHistories.Find(historyId);
        public void Add(RaceHistory history)// Adds a new RaceHistory entity to the database
        {
            _context.RaceHistories.Add(history);
            _context.SaveChanges();
        }
        public void Update(RaceHistory history)// Updates The RaceHistory Table
        {
            _context.RaceHistories.Update(history);
            _context.SaveChanges();
        }
        public void Delete(RaceHistory history)//Deletes an existing History from datbase
        {
            _context.RaceHistories.Remove(history);
            _context.SaveChanges();
        }
    }

}
