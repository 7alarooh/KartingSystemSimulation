using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IGameRepository
    {
        // Add a new Game record to the database
        void Add(Game game);

        // Delete an existing Game record from the database
        void Delete(Game game);

        // Retrieve all Game records from the database
        IEnumerable<Game> GetAll();

        // Retrieve a specific Game record by its unique ID
        Game GetById(int gameId);

        // Update an existing Game record in the database
        void Update(Game game);
    }
}
