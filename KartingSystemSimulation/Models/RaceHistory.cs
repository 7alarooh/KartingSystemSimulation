using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class RaceHistory
    {
        [Key]
        public int HistoryId { get; set; } // Primary Key
        public int RacerId { get; set; } // Foreign Key to Racer
        public TimeSpan BestTiming { get; set; }
        public int StarsEarned { get; set; }

        public Racer Racer { get; set; }
        public int RaceHistoryId { get; set; } // Primary Key
        public ICollection<RaceHistoryLeaderboard> LeaderboardEvaluations { get; set; } // Navigation Property

    }

}
