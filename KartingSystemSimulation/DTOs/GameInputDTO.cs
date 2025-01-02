using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class GameInputDTO
    {
        [Required(ErrorMessage = "RaceType is required.")]
        [StringLength(50, ErrorMessage = "RaceType cannot exceed 50 characters.")]
        public string RaceType { get; set; } // Type of the race (e.g., Kids, Adults, Training)

        [Required(ErrorMessage = "Laps is required.")]
        [Range(10, 15, ErrorMessage = "Laps must be between 1 and 50.")]
        public int Laps { get; set; } // Number of laps in the race

        [Required(ErrorMessage = "RaceDate is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "RaceDate must be a valid date.")]
        public DateTime RaceDate { get; set; } // Date and time of the race

        [Required(ErrorMessage = "KartId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "KartId must be a positive number.")]
        public int KartId { get; set; } // Assigned Kart ID

        [Required(ErrorMessage = "RacerIds is required.")]
        [MinLength(1, ErrorMessage = "At least one RacerId must be provided.")]
        public List<int> RacerIds { get; set; } // List of Racer IDs participating in the race
    }
}
