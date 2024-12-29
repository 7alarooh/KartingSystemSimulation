using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IKartRepository
    {
        // Add a new Kart record to the database
        void Add(Kart kart);

        // Delete an existing Kart record from the database
        void Delete(Kart kart);

        // Retrieve all Kart records from the database
        IEnumerable<Kart> GetAll();

        // Retrieve a specific Kart record by its unique ID
        Kart GetById(int kartId);

        // Update an existing Kart record in the database
        void Update(Kart kart);
    }
}
