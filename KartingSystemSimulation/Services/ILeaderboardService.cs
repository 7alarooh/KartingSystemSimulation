using KartingSystemSimulation.DTOs.LeaderboardDTO_s;
using KartingSystemSimulation.Enums;

namespace KartingSystemSimulation.Services
{
    public interface ILeaderboardService
    {
        LeaderboardOutputDTO AddToLeaderboard(LeaderboardInputDTO input);
        IEnumerable<LeaderboardOutputDTO> GetLeaderboard(Period period);
        void UpdateLeaderboard(int id, LeaderboardInputDTO input);
        void DeleteLeaderboardEntry(int id); 

    }
}