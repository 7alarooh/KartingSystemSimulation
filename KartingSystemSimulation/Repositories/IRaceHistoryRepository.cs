using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceHistoryRepository
    {
        // Add a new RaceHistory record to the database
        void Add(RaceHistory history);

        // Delete an existing RaceHistory record from the database
        void Delete(RaceHistory history);

        // Retrieve all RaceHistory records from the database
        IEnumerable<RaceHistory> GetAll();

        // Retrieve a specific RaceHistory record by its unique ID
        RaceHistory GetById(int historyId);

        // Update an existing RaceHistory record in the database
        void Update(RaceHistory history);
    }
}
