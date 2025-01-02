using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class RaceBooking
    {
        [Key]
        public int BookingId { get; set; } // Primary Key
        public int RacerId { get; set; } // Foreign Key to Racer
        public int RaceId { get; set; } // Foreign Key to Game
        [Required]
        public BookingType BookingType { get; set; } // Free or Paid
        [Required]
        public decimal AmountPaid { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public Racer Racer { get; set; }
        [Required]
        public Game Game { get; set; }
    }

}
