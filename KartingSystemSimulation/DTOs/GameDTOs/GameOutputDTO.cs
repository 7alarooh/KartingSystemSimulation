using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartingSystemSimulation.DTOs.GameDTOs
{
    public class GameOutputDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int GameId { get; set; } // Unique identifier for the game

        [Required]
        [StringLength(50)]
        public string RaceType { get; set; } // Type of the race

        [Required]
        [Range(10, 15)]
        public int Laps { get; set; } // Total number of laps in the race

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RaceDate { get; set; } // Date and time of the race

        [Required]
        [Range(1, int.MaxValue)]
        public int KartId { get; set; } // Assigned Kart ID for the game

        [Required]
        public List<string> RacerNames { get; set; } // List of racer full names

        [Required]
        public List<int> TopRacers { get; set; } // List of IDs for top racers
    }
}
