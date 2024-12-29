using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILiveRaceRepository
    {
        // Add a new LiveRace record to the database
        void Add(LiveRace liveRace);

        // Delete an existing LiveRace record from the database
        void Delete(LiveRace liveRace);

        // Retrieve all LiveRace records from the database
        IEnumerable<LiveRace> GetAll();

        // Retrieve a specific LiveRace record by its unique ID
        LiveRace GetById(int raceId);

        // Update an existing LiveRace record in the database
        void Update(LiveRace liveRace);
    }
}
