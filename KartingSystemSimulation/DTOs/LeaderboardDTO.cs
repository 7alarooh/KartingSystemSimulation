using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class LeaderboardDTO
    {
        [Required(ErrorMessage = "LeaderboardId is required.")]
        public int LeaderboardId { get; set; } // Primary key

        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // Racer associated with the leaderboard

        [Required(ErrorMessage = "Period is required.")]
        [StringLength(10, ErrorMessage = "Period cannot be longer than 10 characters.")]
        [RegularExpression(@"^(Weekly|Monthly|Yearly)$", ErrorMessage = "Period must be either Weekly, Monthly, or Yearly.")]
        public string Period { get; set; } // Period (Weekly, Monthly, Yearly)

        [Required(ErrorMessage = "Best Timing is required.")]
        public TimeSpan BestTiming { get; set; } // Best timing
    }
}
