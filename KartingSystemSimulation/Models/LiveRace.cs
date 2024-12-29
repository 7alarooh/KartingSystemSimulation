using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class LiveRace
    {
        [Key]
        public int LiveRaceId { get; set; } // Primary Key
        public int GameId { get; set; } // Foreign Key to Game
        public int CurrentLap { get; set; }
        public TimeSpan LapTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        
        public Game Game { get; set; } // Navigation Property
        public ICollection<Racer> Racers { get; set; } // Navigation Property for multiple racers
    }
}

