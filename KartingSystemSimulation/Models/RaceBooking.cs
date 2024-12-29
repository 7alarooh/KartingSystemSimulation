using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class RaceBooking
    {
        [Key]
        public int BookingId { get; set; } // Primary Key
        public int RacerId { get; set; } // Foreign Key to Racer
        public int RaceId { get; set; } // Foreign Key to Game
        public string BookingType { get; set; } // Free or Paid
        public decimal AmountPaid { get; set; }
        public DateTime BookingDate { get; set; }

        public Racer Racer { get; set; }
        public Game Game { get; set; }
    }

}
