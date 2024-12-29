using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class GameDTO
    {
        [Required(ErrorMessage = "GameId is required.")]
        public int GameId { get; set; } // Primary key

        [Required(ErrorMessage = "Race Type is required.")]
        [StringLength(50, ErrorMessage = "Race Type cannot be longer than 50 characters.")]
        public string RaceType { get; set; } // Race type (e.g., Kids, Adults, Training)

        [Required(ErrorMessage = "Number of laps is required.")]
        [Range(1, 100, ErrorMessage = "Laps must be between 1 and 100.")]
        public int Laps { get; set; } // Number of laps

        [Required(ErrorMessage = "Race Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime RaceDate { get; set; } // Date of the race

        [Required(ErrorMessage = "KartId is required.")]
        public int KartId { get; set; } // Associated kart
    }
}
