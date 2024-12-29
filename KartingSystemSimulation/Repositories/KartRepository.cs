using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class KartRepository : IKartRepository
    {
        private readonly ApplicationDbContext _context;

        public KartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Kart> GetAll() => _context.Karts.ToList();
        public Kart GetById(int kartId) => _context.Karts.Find(kartId);
        public void Add(Kart kart)
        {
            _context.Karts.Add(kart);
            _context.SaveChanges();
        }
        public void Update(Kart kart)
        {
            _context.Karts.Update(kart);
            _context.SaveChanges();
        }
        public void Delete(Kart kart)
        {
            _context.Karts.Remove(kart);
            _context.SaveChanges();
        }
    }

}
