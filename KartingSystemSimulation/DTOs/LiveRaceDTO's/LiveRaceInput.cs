using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs.LiveRaceDTO_s
{
    public class LiveRaceInputDTO
    {
        [Required]
        public int GameId { get; set; } // Associated game ID

        [Required]
        public DateTime RaceDate { get; set; } // Race start time

        public string UpdateDetails { get; set; } // Additional details
    }

    public class UpdateLapInputDTO
    {
        [Required]
        public int LiveRaceId { get; set; } // ID of the live race

        [Required]
        public int RacerId { get; set; } // Racer's ID

        [Required]
        public int CurrentLap { get; set; } // Updated lap count

        [Required]
        public TimeSpan LapTime { get; set; } // Latest lap time
    }

}
