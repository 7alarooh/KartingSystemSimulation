using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface ILeaderboardRepository
    {
        // Add a new Leaderboard record to the database
        void Add(Leaderboard leaderboard);

        // Delete an existing Leaderboard record from the database
        void Delete(Leaderboard leaderboard);

        // Retrieve all Leaderboard records from the database
        IEnumerable<Leaderboard> GetAll();

        // Retrieve a specific Leaderboard record by its unique ID
        Leaderboard GetById(int leaderboardId);

        // Update an existing Leaderboard record in the database
        void Update(Leaderboard leaderboard);
    }
}
