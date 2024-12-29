using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRacerRepository
    {
        void Add(Racer racer);
        void Delete(Racer racer);
        IEnumerable<Racer> GetAll();
        Racer GetById(int racerId);
        void Update(Racer racer);
    }
}