using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; } // Primary Key
        public RaceType RaceType { get; set; } // e.g., Kids, Adults, Training
        public int Laps { get; set; }
        public string TopRacers { get; set; } // JSON String
        public DateTime RaceDate { get; set; }
        public int KartId { get; set; } // Foreign Key to Kart
        public Kart Kart { get; set; }
        public ICollection<LiveRace> LiveRaceUpdates { get; set; } // Navigation Property
        public int LiveRaceId { get; set; } // FK
    }

}
