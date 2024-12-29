using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(User user);
        IEnumerable<User> GetAll();
        User GetById(int userId);
        void Update(User user);
    }
}