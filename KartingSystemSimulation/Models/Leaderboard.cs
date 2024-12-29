using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class Leaderboard
    {
        [Key]
        public int LeaderboardId { get; set; } // Primary Key
        public int RacerId { get; set; } // Foreign Key to Racer
        public Period Period { get; set; } // e.g., Weekly, Monthly, Yearly
        public TimeSpan BestTiming { get; set; }

        public Racer Racer { get; set; }
        public ICollection<RaceHistoryLeaderboard> RaceHistoryEvaluations { get; set; } // Navigation Property

    }

}
