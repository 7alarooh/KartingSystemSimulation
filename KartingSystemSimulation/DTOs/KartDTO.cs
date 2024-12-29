namespace KartingSystemSimulation.DTOs
{
    public class KartDTO
    {
        public int KartId { get; set; } // Primary key
        public string Type { get; set; } // Kart type (Kids, Adults, Couples, Private)
        public bool Availability { get; set; } // Availability status
    }
}
