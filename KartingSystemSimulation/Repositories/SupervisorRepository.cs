using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private readonly ApplicationDbContext _context; // Database context for accessing the database

        public SupervisorRepository(ApplicationDbContext context) // Constructor to initialize the repository with the database context
        {
            _context = context;
        }

        public IEnumerable<Supervisor> GetAllSupervisors() => _context.Supervisors.ToList(); // Retrieves all Supervisor entities from the database

        public Supervisor GetSupervisorById(int supervisorId) => _context.Supervisors.Find(supervisorId); // Retrieves a specific Supervisor entity by its ID

        public Supervisor AddSupervisor(Supervisor supervisor) // Adds a new Supervisor entity to the database
        {
            _context.Supervisors.Add(supervisor); // Adds the supervisor to the context
            _context.SaveChanges(); // Saves changes to the database

            return supervisor;
        }

        public void UpdateSupervisor(Supervisor supervisor) // Updates an existing Supervisor entity in the database
        {
            _context.Supervisors.Update(supervisor); // Updates the supervisor in the context
            _context.SaveChanges(); // Saves changes to the database
        }

        public void DeleteSupervisor(Supervisor supervisor) // Deletes a Supervisor entity from the database
        {
            _context.Supervisors.Remove(supervisor); // Removes the supervisor from the context
            _context.SaveChanges(); // Saves changes to the database
        }
    }
}
