using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RaceHistoryDTO
    {
        [Required(ErrorMessage = "HistoryId is required.")]
        public int HistoryId { get; set; } // Primary key

        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // Racer associated with the history

        [Required(ErrorMessage = "BestTiming is required.")]
        public TimeSpan BestTiming { get; set; } // Best lap timing

        [Required(ErrorMessage = "StarsEarned is required.")]
        [Range(0, 5, ErrorMessage = "StarsEarned must be between 0 and 5.")]
        public int StarsEarned { get; set; } // Stars earned (Assuming max of 5 stars)
    }
}
