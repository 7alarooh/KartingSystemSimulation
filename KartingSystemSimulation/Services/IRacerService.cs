using KartingSystemSimulation.Models;
using System;
using System.Collections.Generic;

namespace KartingSystemSimulation.Services
{
    public interface IRacerService
    {
        IEnumerable<Racer> GetAll();
        Racer GetById(int racerId);
        void Add(Racer racer, int currentUserId);  // Add method includes currentUserId for validation
        void Update(Racer racer);
        void Delete(int racerId, int currentUserId);  // Delete method with currentUserId for self-check validation
    }
}
