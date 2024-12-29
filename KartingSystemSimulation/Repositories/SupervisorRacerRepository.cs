using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class SupervisorRacerRepository : ISupervisorRacerRepository
    {
        private readonly ApplicationDbContext _context;

        public SupervisorRacerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SupervisorRacer> GetAll() => _context.SupervisorRacers.ToList();
        public SupervisorRacer GetById(int supervisorId, int racerId)
            => _context.SupervisorRacers.Find(supervisorId, racerId);
        public void Add(SupervisorRacer supervisorRacer)
        {
            _context.SupervisorRacers.Add(supervisorRacer);
            _context.SaveChanges();
        }
        public void Delete(SupervisorRacer supervisorRacer)
        {
            _context.SupervisorRacers.Remove(supervisorRacer);
            _context.SaveChanges();
        }
    }

}
