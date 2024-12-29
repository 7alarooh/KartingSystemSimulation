using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class LiveRaceDTO
    {
        [Required(ErrorMessage = "RaceId is required.")]
        public int RaceId { get; set; } // Foreign key to Game

        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // Racer participating

        [Required(ErrorMessage = "Current lap is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Current lap must be a positive number.")]
        public int CurrentLap { get; set; } // Current lap number

        [Required(ErrorMessage = "Lap time is required.")]
        public TimeSpan LapTime { get; set; } // Timing for the current lap

        [Required(ErrorMessage = "Total time is required.")]
        public TimeSpan TotalTime { get; set; } // Cumulative race timing
    }
}
