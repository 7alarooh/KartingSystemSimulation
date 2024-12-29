namespace KartingSystemSimulation.DTOs
{
    public class LiveRaceDTO
    {
        public int RaceId { get; set; } // Foreign key to Game
        public int RacerId { get; set; } // Racer participating
        public int CurrentLap { get; set; } // Current lap number
        public TimeSpan LapTime { get; set; } // Timing for the current lap
        public TimeSpan TotalTime { get; set; } // Cumulative race timing
    }
}
