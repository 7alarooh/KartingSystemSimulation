using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RaceBookingDTO
    {
        [Required]
        public int GameId { get; set; }
        [Required(ErrorMessage = "BookingId is required.")]
        public int BookingId { get; set; } // Primary key

        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // Racer associated with the booking

        [Required(ErrorMessage = "RaceId is required.")]
        public int RaceId { get; set; } // Game associated with the booking

        [Required(ErrorMessage = "BookingType is required.")]
        [StringLength(10, ErrorMessage = "Booking Type cannot be longer than 10 characters.")]
        [RegularExpression(@"^(Free|Paid)$", ErrorMessage = "Booking Type must be either Free or Paid.")]
        public string BookingType { get; set; } // Booking type (Free, Paid)

        [Required(ErrorMessage = "AmountPaid is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "AmountPaid must be greater than 0.")]
        public decimal AmountPaid { get; set; } // Amount paid for booking

        [Required(ErrorMessage = "BookingDate is required.")]
        public DateTime BookingDate { get; set; } // Booking date
    }
}
