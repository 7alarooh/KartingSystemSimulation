using KartingSystemSimulation.DTOs.LiveRaceDTO_s;

namespace KartingSystemSimulation.Services
{
    public interface ILiveRaceService
    {
        void AddLiveRace(LiveRaceInputDTO liveRaceInput);
        IEnumerable<LiveRaceOutputDTO> GetAllLiveRaces();
        LiveRaceOutputDTO GetLiveRaceById(int liveRaceId);
        IEnumerable<LiveRaceRacerOutputDTO> GetRaceRacers(int liveRaceId);
        void UpdateLapTime(UpdateLapInputDTO updateLapInput);
    }
}