using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IAdminService
    {
        void AddAdmin(AdminInputDTO adminDto);
        void Delete(int adminId);
        IEnumerable<AdminOutputDTO> GetAll();
        AdminOutputDTO GetById(int adminId);
        void Update(int adminId, AdminInputDTO adminInput);
    }
}