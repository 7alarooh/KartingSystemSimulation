using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RacerRepository
    {
        private readonly ApplicationDbContext _context;

        public RacerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Racer> GetAll() => _context.Racers.ToList();
        public Racer GetById(int racerId) => _context.Racers.Find(racerId);
        public void Add(Racer racer)
        {
            _context.Racers.Add(racer);
            _context.SaveChanges();
        }
        public void Update(Racer racer)
        {
            _context.Racers.Update(racer);
            _context.SaveChanges();
        }
        public void Delete(Racer racer)
        {
            _context.Racers.Remove(racer);
            _context.SaveChanges();
        }
    }

}
