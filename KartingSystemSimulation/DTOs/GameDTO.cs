namespace KartingSystemSimulation.DTOs
{
    public class GameDTO
    {
        public int GameId { get; set; } // Primary key
        public string RaceType { get; set; } // Race type (e.g., Kids, Adults, Training)
        public int Laps { get; set; } // Number of laps
        public DateTime RaceDate { get; set; } // Date of the race
        public int KartId { get; set; } // Associated kart
    }
}
