using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class LiveRaceRepository : ILiveRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public LiveRaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LiveRace> GetAll() => _context.LiveRaces.ToList();
        public LiveRace GetById(int raceId) => _context.LiveRaces.Find(raceId);
        public void Add(LiveRace liveRace)
        {
            _context.LiveRaces.Add(liveRace);
            _context.SaveChanges();
        }
        public void Update(LiveRace liveRace)
        {
            _context.LiveRaces.Update(liveRace);
            _context.SaveChanges();
        }
        public void Delete(LiveRace liveRace)
        {
            _context.LiveRaces.Remove(liveRace);
            _context.SaveChanges();
        }
    }

}
