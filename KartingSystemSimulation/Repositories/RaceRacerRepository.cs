using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceRacerRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRacerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceRacer> GetAll() => _context.RaceRacers.ToList();
        public RaceRacer GetById(int raceId, int racerId)
            => _context.RaceRacers.Find(raceId, racerId);
        public void Add(RaceRacer raceRacer)
        {
            _context.RaceRacers.Add(raceRacer);
            _context.SaveChanges();
        }
        public void Delete(RaceRacer raceRacer)
        {
            _context.RaceRacers.Remove(raceRacer);
            _context.SaveChanges();
        }
    }

}
