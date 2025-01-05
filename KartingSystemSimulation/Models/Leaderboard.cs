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

        public TimeSpan BestTiming { get; set; } // Best timing for the racer in the leaderboard period

        public int StarsEarned { get; set; } // Total stars earned by the racer

        public int Position { get; set; } // Racer's position in the leaderboard

        // Navigation properties
        public Racer Racer { get; set; } // Reference to the Racer entity

        public ICollection<RaceHistoryLeaderboard> RaceHistoryEvaluations { get; set; } // Navigation property for evaluations
    }


}
