using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IRacerService
    {
        void AddRacer(RacerInputDTO racerInput);
        int CalculateAge(DateTime dob);
        void DeleteRacer(int id);
        IEnumerable<RacerOutputDTO> GetAllRacers();
        RacerOutputDTO GetRacerById(int id);
        bool IsSupervisorRelatedToRacer(string supervisorEmail, int racerId);
        void UpdateRacer(int id, RacerInputDTO racerInput);
    }
}