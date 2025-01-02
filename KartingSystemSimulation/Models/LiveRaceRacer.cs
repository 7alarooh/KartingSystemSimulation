using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class LiveRaceRacer
    {
        [Key]
        public int LiveRaceRacerId { get; set; } // Primary Key

        public int LiveRaceId { get; set; } // Foreign Key to LiveRace

        public int RacerId { get; set; } // Foreign Key to Racer

        public int CurrentLap { get; set; } // Current lap for the racer

        public TimeSpan LapTime { get; set; } // Latest lap time for the racer

        public TimeSpan TotalTime { get; set; } // Cumulative time for the racer

        public LiveRace LiveRace { get; set; } // Navigation property to LiveRace

        public Racer Racer { get; set; } // Navigation property to Racer
    }
}
