using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class KartRepository : IKartRepository
    {
        private readonly ApplicationDbContext _context;// Database context for accessing the database


        public KartRepository(ApplicationDbContext context) // Constructor to initialize the repository with the database context
        {
            _context = context;
        }

        public IEnumerable<Kart> GetAllKarts() => _context.Karts.ToList();// Retrieves all Kart entities from the database

        public Kart GetKartById(int kartId) => _context.Karts.Find(kartId);// Retrieves a specific Kart entity by its ID
        public void AddKart(Kart kart)// Adds a new Kart entity to the database
        {
            _context.Karts.Add(kart);
            _context.SaveChanges();
        }
        public void UpdateKart(Kart kart)// Updates an existing Kart entity in the database
        {
            _context.Karts.Update(kart);
            _context.SaveChanges();
        }
        public void DeleteKart(Kart kart)// Deletes a Kart entity from the database
        {
            _context.Karts.Remove(kart);
            _context.SaveChanges();
        }
    }

}
