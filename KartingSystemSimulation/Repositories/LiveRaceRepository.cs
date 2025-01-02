using KartingSystemSimulation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KartingSystemSimulation.Repositories
{
    public class LiveRaceRepository : ILiveRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public LiveRaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LiveRace> GetAllLiveRaces() => _context.LiveRaces.ToList();

        public LiveRace GetLiveRaceById(int liveRaceId) => _context.LiveRaces.Find(liveRaceId);

        public LiveRace GetByIdWithRacers(int liveRaceId)
        {
            return _context.LiveRaces
                .Include(lr => lr.LiveRaceRacers)
                .ThenInclude(lrr => lrr.Racer)
                .FirstOrDefault(lr => lr.LiveRaceId == liveRaceId);
        }

        public void AddLiveRace(LiveRace liveRace)
        {
            _context.LiveRaces.Add(liveRace);
            _context.SaveChanges();
        }

        public void UpdateLiveRace(LiveRace liveRace)
        {
            _context.LiveRaces.Update(liveRace);
            _context.SaveChanges();
        }

        public void DeleteLiveRace(LiveRace liveRace)
        {
            _context.LiveRaces.Remove(liveRace);
            _context.SaveChanges();
        }
    }


}
