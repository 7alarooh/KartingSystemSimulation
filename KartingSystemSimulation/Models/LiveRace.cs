using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class LiveRace
    {
        [Key]
        public int LiveRaceId { get; set; } // Primary Key

        public int GameId { get; set; } // Foreign Key to Game

        public DateTime RaceDate { get; set; } // Timestamp for when the race starts

        public string UpdateDetails { get; set; } // General updates or information about the race

        public Game Game { get; set; } // Navigation Property to Game

        public ICollection<LiveRaceRacer> LiveRaceRacers { get; set; } // Navigation Property to track racers in the race
    }
}

