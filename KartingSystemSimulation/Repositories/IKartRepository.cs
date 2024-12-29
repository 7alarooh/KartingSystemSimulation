using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IKartRepository
    {
        void AddKart(Kart kart);
        void DeleteKart(Kart kart);
        IEnumerable<Kart> GetAllKarts();
        Kart GetKartById(int kartId);
        void UpdateKart(Kart kart);
    }
}