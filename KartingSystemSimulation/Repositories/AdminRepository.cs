using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class AdminRepository// This is to handle Admin Repository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> GetAll() => _context.Admins.ToList();
        public Admin GetById(int adminId) => _context.Admins.Find(adminId);
        public void Add(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
        public void Update(Admin admin)
        {
            _context.Admins.Update(admin);
            _context.SaveChanges();
        }
        public void Delete(Admin admin)
        {
            _context.Admins.Remove(admin);
            _context.SaveChanges();
        }
    }
}
