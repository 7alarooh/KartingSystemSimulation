using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IAdminRepository
    {
        void Add(Admin admin);
        void Delete(Admin admin);
        IEnumerable<Admin> GetAll();
        Admin GetById(int adminId);
        void Update(Admin admin);
    }
}