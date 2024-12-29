namespace KartingSystemSimulation.DTOs
{
    public class RaceHistoryDTO
    {
        public int HistoryId { get; set; } // Primary key
        public int RacerId { get; set; } // Racer associated with the history
        public TimeSpan BestTiming { get; set; } // Best lap timing
        public int StarsEarned { get; set; } // Stars earned
    }
}
