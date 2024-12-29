using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IAdminRepository
    {
        void AddAdmin(Admin admin);
        void DeleteAdmin(Admin admin);
        Admin GetAdminById(int adminId);
        IEnumerable<Admin> GetAllAdmins();
        void UpdateAdmin(Admin admin);
    }
}