using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IRacerService
    {
        void AddRacer(RacerInputDTO racerInput);
        void DeleteRacer(int id);
        IEnumerable<RacerOutputDTO> GetAllRacers();
        RacerOutputDTO GetRacerById(int id);
        void UpdateRacer(int id, RacerInputDTO racerInput);
    }
}