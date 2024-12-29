using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class SupervisorRepository
    {
        private readonly ApplicationDbContext _context;

        public SupervisorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supervisor> GetAll() => _context.Supervisors.ToList();
        public Supervisor GetById(int supervisorId) => _context.Supervisors.Find(supervisorId);
        public void Add(Supervisor supervisor)
        {
            _context.Supervisors.Add(supervisor);
            _context.SaveChanges();
        }
        public void Update(Supervisor supervisor)
        {
            _context.Supervisors.Update(supervisor);
            _context.SaveChanges();
        }
        public void Delete(Supervisor supervisor)
        {
            _context.Supervisors.Remove(supervisor);
            _context.SaveChanges();
        }
    }

}
