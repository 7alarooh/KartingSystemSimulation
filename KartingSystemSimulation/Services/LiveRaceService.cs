using KartingSystemSimulation.DTOs.LiveRaceDTO_s;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class LiveRaceService : ILiveRaceService
    {
        private readonly ILiveRaceRepository _liveRaceRepository;
        private readonly IGameRepository _gameRepository;

        public LiveRaceService(ILiveRaceRepository liveRaceRepository, IGameRepository gameRepository)
        {
            _liveRaceRepository = liveRaceRepository;
            _gameRepository = gameRepository;
        }

        // Get all live races
        public IEnumerable<LiveRaceOutputDTO> GetAllLiveRaces()
        {
            var liveRaces = _liveRaceRepository.GetAllLiveRaces();
            if (liveRaces == null || !liveRaces.Any())
            {
                Console.WriteLine("No live races found.");
                return Enumerable.Empty<LiveRaceOutputDTO>();
            }

            return liveRaces.Select(lr => new LiveRaceOutputDTO
            {
                LiveRaceId = lr.LiveRaceId,
                GameId = lr.GameId,
                RaceDate = lr.RaceDate,
                UpdateDetails = lr.UpdateDetails,
                Racers = lr.LiveRaceRacers?.Select(r => new LiveRaceRacerOutputDTO
                {
                    RacerId = r.RacerId,
                    CurrentLap = r.CurrentLap,
                    LapTime = r.LapTime,
                    TotalTime = r.TotalTime
                }).ToList() ?? new List<LiveRaceRacerOutputDTO>()
            });
        }

        // Get live race by ID
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

        // Adds a new live race to the system
        public void AddLiveRace(LiveRaceInputDTO liveRaceInput)
        {
            // Validate that the input DTO is not null
            if (liveRaceInput == null)
                throw new ArgumentNullException(nameof(liveRaceInput), "LiveRaceInputDTO cannot be null.");

            // Validate that the associated game exists
            var game = _gameRepository.GetGameById(liveRaceInput.GameId);
            if (game == null)
                throw new ArgumentException($"Game with ID {liveRaceInput.GameId} does not exist.", nameof(liveRaceInput.GameId));

            // Map the input DTO to the LiveRace model
            var liveRace = new LiveRace
            {
                GameId = liveRaceInput.GameId,           // Associate the live race with the provided game ID
                RaceDate = liveRaceInput.RaceDate,       // Set the race date from the input
                UpdateDetails = liveRaceInput.UpdateDetails // Include any update details
            };

            // Add the live race to the database
            _liveRaceRepository.AddLiveRace(liveRace);

            // Log the addition of the live race (optional)
            Console.WriteLine($"New live race added with ID {liveRace.LiveRaceId} for Game ID {liveRace.GameId}.");
        }


        // Update lap time for a racer
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

        // Get all racers in a live race
        public IEnumerable<LiveRaceRacerOutputDTO> GetRaceRacers(int liveRaceId)
        {
            var liveRace = _liveRaceRepository.GetByIdWithRacers(liveRaceId);
            if (liveRace == null) throw new ArgumentException("Live race not found.");

            return liveRace.LiveRaceRacers.Select(r => new LiveRaceRacerOutputDTO
            {
                RacerId = r.RacerId,
                CurrentLap = r.CurrentLap,
                LapTime = r.LapTime,
                TotalTime = r.TotalTime
            });
        }
    }




}
