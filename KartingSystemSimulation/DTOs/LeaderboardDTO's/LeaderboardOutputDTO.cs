using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartingSystemSimulation.DTOs.LeaderboardDTO_s
{
    public class LeaderboardOutputDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaderboardId { get; set; } // Unique ID for the leaderboard entry
        public int RacerId { get; set; } // ID of the racer
        public string RacerName { get; set; } // Full name of the racer, 
        public Period Period { get; set; } // Leaderboard period (e.g., Weekly, Monthly, Yearly)
        public TimeSpan BestTiming { get; set; } // Best lap timing
        public int StarsEarned { get; set; } // Total stars earned during the period
        public int Position { get; set; } // Racer's position on the leaderboard
    }

}
