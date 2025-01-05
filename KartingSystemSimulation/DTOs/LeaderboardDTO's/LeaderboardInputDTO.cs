using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs.LeaderboardDTO_s
{
    public class LeaderboardInputDTO
    {
        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // ID of the racer

        [Required(ErrorMessage = "Period is required.")]
        public string Period { get; set; } // Period (Weekly, Monthly, Yearly)

        [Required(ErrorMessage = "BestTiming is required.")]
        [RegularExpression(@"^(\d{2}):(\d{2}):(\d{2})$", ErrorMessage = "Invalid BestTiming format. Use hh:mm:ss.")]
        public string BestTiming { get; set; } // Input as string for validation
    }


}
