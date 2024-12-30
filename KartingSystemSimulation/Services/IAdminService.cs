using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IAdminService
    {
        void Add(AdminInputDTO adminInput);
        void Delete(int adminId);
        IEnumerable<AdminOutputDTO> GetAll();
        AdminOutputDTO GetById(int adminId);
        void Update(int adminId, AdminInputDTO adminInput);
    }
}