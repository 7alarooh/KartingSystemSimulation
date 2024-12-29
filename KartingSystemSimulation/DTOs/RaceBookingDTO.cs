namespace KartingSystemSimulation.DTOs
{
    public class RaceBookingDTO
    {
        public int BookingId { get; set; } // Primary key
        public int RacerId { get; set; } // Racer associated with the booking
        public int RaceId { get; set; } // Game associated with the booking
        public string BookingType { get; set; } // Booking type (Free, Paid)
        public decimal AmountPaid { get; set; } // Amount paid for booking
        public DateTime BookingDate { get; set; } // Booking date
    }
}
