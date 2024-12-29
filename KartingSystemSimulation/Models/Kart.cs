using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class Kart
    {
        [Key]
        public int KartId { get; set; } // Primary Key
        public string Type { get; set; } // e.g., Kids, Adults
        public bool Availability { get; set; }
    }
}
