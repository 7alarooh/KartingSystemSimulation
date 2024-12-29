using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IKartRepository
    {
        void Add(Kart kart);
        void Delete(Kart kart);
        IEnumerable<Kart> GetAll();
        Kart GetById(int kartId);
        void Update(Kart kart);
    }
}