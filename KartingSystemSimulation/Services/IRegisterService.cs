using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IRegisterService
    {
        void RegisterAdmin(AdminInputDTO adminDto);
    }
}