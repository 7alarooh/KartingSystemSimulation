﻿using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        void UpdateUser(User user);
    }
}