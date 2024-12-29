﻿using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceRacerRepository : IRaceRacerRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRacerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceRacer> GetAll() => _context.RaceRacers.ToList();
        public RaceRacer GetById(int raceId, int racerId)
            => _context.RaceRacers.Find(raceId, racerId);// Retrieves a specific RaceRacer entity by its raceId and racerId
        public void Add(RaceRacer raceRacer)// Adds a new RaceRacer entity to the database
        {
            _context.RaceRacers.Add(raceRacer);
            _context.SaveChanges();
        }
        public void Delete(RaceRacer raceRacer)// Deletes a RaceRacer entity from the database
        {
            _context.RaceRacers.Remove(raceRacer);
            _context.SaveChanges();
        }
    }

}
