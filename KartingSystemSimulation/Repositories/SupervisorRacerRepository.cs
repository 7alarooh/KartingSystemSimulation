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

        public IEnumerable<SupervisorRacer> GetAllSupervisorRacers() => _context.SupervisorRacers.ToList();
        public SupervisorRacer GetSupervisorRacerById(int supervisorId, int racerId)
    => _context.SupervisorRacers.FirstOrDefault(sr => sr.SupervisorId == supervisorId && sr.RacerId == racerId);
        public void AddSupervisorRacer(SupervisorRacer supervisorRacer)
        {
            _context.SupervisorRacers.Add(supervisorRacer);
            _context.SaveChanges();
        }
        public void DeleteSupervisorRacer(SupervisorRacer supervisorRacer)
        {
            _context.SupervisorRacers.Remove(supervisorRacer);
            _context.SaveChanges();
        }
       
    }

}
