using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceRacerRepository
    {
        // Add a new RaceRacer record to the database
        void Add(RaceRacer raceRacer);

        // Delete an existing RaceRacer record from the database
        void Delete(RaceRacer raceRacer);

        // Retrieve all RaceRacer records from the database
        IEnumerable<RaceRacer> GetAll();

        // Retrieve a specific RaceRacer record by the composite key (RaceId and RacerId)
        RaceRacer GetById(int raceId, int racerId);
    }
}
