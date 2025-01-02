using KartingSystemSimulation.DTOs.LiveRaceDTO_s;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class LiveRaceService : ILiveRaceService
    {
        private readonly ILiveRaceRepository _liveRaceRepository;

        public LiveRaceService(ILiveRaceRepository liveRaceRepository)
        {
            _liveRaceRepository = liveRaceRepository;
        }

        public IEnumerable<LiveRaceOutputDTO> GetAllLiveRaces()
        {
            var liveRaces = _liveRaceRepository.GetAllLiveRaces();
            return liveRaces.Select(lr => new LiveRaceOutputDTO
            {
                LiveRaceId = lr.LiveRaceId,
                GameId = lr.GameId,
                RaceDate = lr.RaceDate,
                UpdateDetails = lr.UpdateDetails,
                Racers = lr.LiveRaceRacers.Select(r => new LiveRaceRacerOutputDTO
                {
                    RacerId = r.RacerId,
                    CurrentLap = r.CurrentLap,
                    LapTime = r.LapTime,
                    TotalTime = r.TotalTime
                }).ToList()
            });
        }

        public LiveRaceOutputDTO GetLiveRaceById(int liveRaceId)
        {
            var liveRace = _liveRaceRepository.GetByIdWithRacers(liveRaceId);
            if (liveRace == null) throw new ArgumentException("Live race not found.");

            return new LiveRaceOutputDTO
            {
                LiveRaceId = liveRace.LiveRaceId,
                GameId = liveRace.GameId,
                RaceDate = liveRace.RaceDate,
                UpdateDetails = liveRace.UpdateDetails,
                Racers = liveRace.LiveRaceRacers.Select(r => new LiveRaceRacerOutputDTO
                {
                    RacerId = r.RacerId,
                    CurrentLap = r.CurrentLap,
                    LapTime = r.LapTime,
                    TotalTime = r.TotalTime
                }).ToList()
            };
        }

        public void AddLiveRace(LiveRaceInputDTO liveRaceInput)
        {
            var liveRace = new LiveRace
            {
                GameId = liveRaceInput.GameId,
                RaceDate = liveRaceInput.RaceDate,
                UpdateDetails = liveRaceInput.UpdateDetails
            };

            _liveRaceRepository.AddLiveRace(liveRace);
        }

        public void UpdateLapTime(UpdateLapInputDTO updateLapInput)
        {
            var liveRace = _liveRaceRepository.GetByIdWithRacers(updateLapInput.LiveRaceId);
            if (liveRace == null) throw new ArgumentException("Live race not found.");

            var racer = liveRace.LiveRaceRacers.FirstOrDefault(r => r.RacerId == updateLapInput.RacerId);
            if (racer == null) throw new ArgumentException("Racer not found in this live race.");

            racer.CurrentLap = updateLapInput.CurrentLap;
            racer.LapTime = updateLapInput.LapTime;
            racer.TotalTime += updateLapInput.LapTime;

            _liveRaceRepository.UpdateLiveRace(liveRace);
        }
    }



}
