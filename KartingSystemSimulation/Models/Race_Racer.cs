using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class RaceRacer
    {
        [Key]
        public int RaceId { get; set; } // Foreign Key to Game
        public int RacerId { get; set; } // Foreign Key to Racer

        public Game Game { get; set; }
        public Racer Racer { get; set; }
    }

}
