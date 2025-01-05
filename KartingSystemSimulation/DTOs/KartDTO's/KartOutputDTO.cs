using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
   
    public class KartOutputDTO
    {
        [Required]
        public int KartId { get; set; } // Unique identifier for the kart

        [Required]
        [StringLength(50)]
        public KartType KartType { get; set; } // Type of kart

        [Required]
        public bool Availability { get; set; } // Availability status
    }
}
