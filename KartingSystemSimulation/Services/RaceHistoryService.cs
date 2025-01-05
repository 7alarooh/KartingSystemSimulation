using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class RaceHistoryService : IRaceHistoryService
    {
        private readonly ILiveRaceService _liveRaceService;
        private readonly IRaceHistoryRepository _raceHistoryRepository;

        public RaceHistoryService(ILiveRaceService liveRaceService, IRaceHistoryRepository raceHistoryRepository)
        {
            _liveRaceService = liveRaceService; // Inject LiveRaceService
            _raceHistoryRepository = raceHistoryRepository; // Inject RaceHistoryRepository
        }

        // Add racer history from live race
        public void AddRacerHistoryFromLiveRace(int liveRaceId)
        {
            // Get all racers from the specified live race
            var racers = _liveRaceService.GetRaceRacers(liveRaceId);
            if (racers == null || !racers.Any()) throw new ArgumentException("No racers found in this live race.");

            // Iterate over each racer and add to race history
            foreach (var racer in racers)
            {
                // Create a new RaceHistory object
                var raceHistory = new RaceHistory
                {
                    RacerId = racer.RacerId,
                    BestTiming = racer.LapTime, // Assuming LapTime is the best timing in this context
                    StarsEarned = CalculateStars(racer.TotalTime), // Calculate stars based on performance
                };

                // Save the race history in the database
                _raceHistoryRepository.AddRaceHistory(raceHistory);
            }

            Console.WriteLine($"Race history successfully added for LiveRaceId {liveRaceId}");
        }

        // Calculate stars based on total time
        private int CalculateStars(TimeSpan totalTime)
        {
            if (totalTime.TotalSeconds < 60) return 5; // 5 stars for under 1 minute
            if (totalTime.TotalSeconds < 120) return 4; // 4 stars for under 2 minutes
            if (totalTime.TotalSeconds < 180) return 3; // 3 stars for under 3 minutes
            if (totalTime.TotalSeconds < 240) return 2; // 2 stars for under 4 minutes
            return 1; // 1 star for over 4 minutes
        }
        // Fetch all race histories
        public IEnumerable<RaceHistory> GetAllRaceHistories()
        {
            return _raceHistoryRepository.GetAllRaceHistories();
        }

        // Fetch race history by ID
        public RaceHistory GetRaceHistoryById(int historyId)
        {
            return _raceHistoryRepository.GetRaceHistoryById(historyId);
        }


    }
}
