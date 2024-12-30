using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class KartInputDTO
    {
        [Required(ErrorMessage = "KartId is required.")]
        public int KartId { get; set; } // Primary key

        [Required(ErrorMessage = "Kart Type is required.")]
        [StringLength(20, ErrorMessage = "Kart Type cannot be longer than 20 characters.")]
        public KartType KartType { get; set; } // Kart type (Kids, Adults, Couples, Private)

        [Required(ErrorMessage = "Availability status is required.")]
        public bool Availability { get; set; } // Availability status
    }
}
