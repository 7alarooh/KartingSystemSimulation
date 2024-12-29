namespace KartingSystemSimulation.DTOs
{
    public class LeaderboardDTO
    {
        public int LeaderboardId { get; set; } // Primary key
        public int RacerId { get; set; } // Racer associated with the leaderboard
        public string Period { get; set; } // Period (Weekly, Monthly, Yearly)
        public TimeSpan BestTiming { get; set; } // Best timing
    }
}
