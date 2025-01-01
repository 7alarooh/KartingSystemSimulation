using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Services
{
    public interface IUserService
    {
        void AddUser(UserInputDTO userDto);
        void Delete(int userId, string adminEmail);
        IEnumerable<UserOutputDTO> GetAll();
        UserOutputDTO GetById(int userId);
        void Update(int userId, UserInputDTO userDto);

        // Added for testing only
        User TestAddUser(UserInputDTO userInputDTO);
    }
}