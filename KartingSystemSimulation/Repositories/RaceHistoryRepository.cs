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
        public void Add(RaceHistory history)
        {
            _context.RaceHistories.Add(history);
            _context.SaveChanges();
        }
        public void Update(RaceHistory history)
        {
            _context.RaceHistories.Update(history);
            _context.SaveChanges();
        }
        public void Delete(RaceHistory history)
        {
            _context.RaceHistories.Remove(history);
            _context.SaveChanges();
        }
    }

}
