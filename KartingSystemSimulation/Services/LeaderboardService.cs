using KartingSystemSimulation.DTOs.LeaderboardDTO_s;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepository _leaderboardRepository; // Leaderboard repository
        private readonly IRacerRepository _racerRepository; // Racer repository for related data

        public LeaderboardService(ILeaderboardRepository leaderboardRepository, IRacerRepository racerRepository)
        {
            _leaderboardRepository = leaderboardRepository; // Inject leaderboard repository
            _racerRepository = racerRepository; // Inject racer repository
        }

        // Add a racer to the leaderboard
        public LeaderboardOutputDTO AddToLeaderboard(LeaderboardInputDTO input)
        {
            // Validate the Period
            if (!Enum.TryParse(input.Period, true, out Period periodEnum))
            {
                throw new ArgumentException("Invalid period specified.");
            }

            // Parse BestTiming string to TimeSpan
            if (!TimeSpan.TryParse(input.BestTiming, out TimeSpan bestTiming))
            {
                throw new ArgumentException("Invalid BestTiming format. Use hh:mm:ss.");
            }

            // Validate Racer existence
            var racer = _racerRepository.GetRacerById(input.RacerId);
            if (racer == null)
                throw new InvalidOperationException("Racer not found.");

            // Create Leaderboard entry
            var leaderboard = new Leaderboard
            {
                RacerId = input.RacerId,
                Period = periodEnum, // Correctly parsed enum value
                BestTiming = bestTiming,
                Position = 0 // Position will be updated dynamically
            };

            _leaderboardRepository.AddLeaderboard(leaderboard); // Save the leaderboard entry

            return new LeaderboardOutputDTO
            {
                LeaderboardId = leaderboard.LeaderboardId,
                RacerId = leaderboard.RacerId,
                RacerName = $"{racer.FirstName} {racer.LastName}",
                Period = leaderboard.Period,
                BestTiming = leaderboard.BestTiming,
                Position = leaderboard.Position
            };
        }

        // Get leaderboard for a specific period
        public IEnumerable<LeaderboardOutputDTO> GetLeaderboard(Period period)
        {
            var leaderboardEntries = _leaderboardRepository.GetLeaderboardByPeriod(period);

            return leaderboardEntries.Select(l =>
            {
                var racer = _racerRepository.GetRacerById(l.RacerId); // Get racer details
                return new LeaderboardOutputDTO
                {
                    LeaderboardId = l.LeaderboardId,
                    RacerId = l.RacerId,
                    RacerName = $"{racer.FirstName} {racer.LastName}",
                    Period = l.Period,
                    BestTiming = l.BestTiming,
                    Position = l.Position
                };
            }).OrderBy(e => e.BestTiming).ToList(); // Sort by best timing
        }

        // Update a leaderboard entry
        public void UpdateLeaderboard(int id, LeaderboardInputDTO input)
        {
            // Retrieve the leaderboard entry
            var leaderboard = _leaderboardRepository.GetLeaderboardById(id);
            if (leaderboard == null)
                throw new InvalidOperationException("Leaderboard entry not found.");

            // Validate and parse the Period
            if (!Enum.TryParse(input.Period, true, out Period periodEnum))
            {
                throw new ArgumentException("Invalid period specified.");
            }

            // Parse BestTiming string to TimeSpan
            if (!TimeSpan.TryParse(input.BestTiming, out TimeSpan bestTiming))
            {
                throw new ArgumentException("Invalid BestTiming format. Use hh:mm:ss.");
            }

            // Update the leaderboard entry
            leaderboard.BestTiming = bestTiming;
            leaderboard.Period = periodEnum;

            _leaderboardRepository.UpdateLeaderboard(leaderboard); // Save the updated leaderboard entry
        }
        public void DeleteLeaderboardEntry(int id)
        {
            var leaderboard = _leaderboardRepository.GetLeaderboardById(id);
            if (leaderboard == null)
                throw new InvalidOperationException("Leaderboard entry not found.");

            _leaderboardRepository.DeleteLeaderboard(leaderboard);
        }
    }

}
