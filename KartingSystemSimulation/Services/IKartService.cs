using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IKartService
    {
        void AddKart(KartInputDTO kartInput);
        void DeleteKart(int id);
        IEnumerable<KartOutputDTO> GetAllKarts();
        KartOutputDTO GetKartById(int id);
        void UpdateKart(int id, KartInputDTO kartInput);
    }
}