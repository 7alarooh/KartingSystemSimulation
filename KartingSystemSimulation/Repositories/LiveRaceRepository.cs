using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class LiveRaceRepository : ILiveRaceRepository
    {
        private readonly ApplicationDbContext _context;// Database context for accessing the database


        public LiveRaceRepository(ApplicationDbContext context)// Constructor to initialize the repository with the database context
        {
            _context = context;
        }

        public IEnumerable<LiveRace> GetAllLiveRaces() => _context.LiveRaces.ToList();
        public LiveRace GetLiveRaceById(int raceId) => _context.LiveRaces.Find(raceId);
        public void AddLiveRaces(LiveRace liveRace)
        {
            _context.LiveRaces.Add(liveRace);// Adds a new LiveRace entity to the database
            _context.SaveChanges();
        }
        public void UpdateLiveRace(LiveRace liveRace)// Updates an existing LiveRace entity in the database
        {
            _context.LiveRaces.Update(liveRace);
            _context.SaveChanges();
        }
        public void DeleteLiveRace(LiveRace liveRace)// Deletes a LiveRace entity from the database
        {
            _context.LiveRaces.Remove(liveRace);
            _context.SaveChanges();
        }
    }

}
