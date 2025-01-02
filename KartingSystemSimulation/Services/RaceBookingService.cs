using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    // Service for handling RaceBooking
    public class RaceBookingService : IRaceBookingService
    {
        private readonly IRaceBookingRepository _raceBookingRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IRacerRepository _racerRepository;

        public RaceBookingService(IRaceBookingRepository raceBookingRepository, IGameRepository gameRepository, IRacerRepository racerRepository)
        {
            _raceBookingRepository = raceBookingRepository;
            _gameRepository = gameRepository;
            _racerRepository = racerRepository;
        }

        // Create a new Race Booking
        public async Task<RaceBookingDTO> CreateRaceBookingAsync(RaceBookingDTO bookingDTO)
        {
            // Validate Game existence
            var game = _gameRepository.GetGameById(bookingDTO.RaceId);
            if (game == null)
                throw new ArgumentException($"Game with ID {bookingDTO.RaceId} does not exist.");

            // Validate Racer existence
            var racer = _racerRepository.GetRacerById(bookingDTO.RacerId);
            if (racer == null)
                throw new ArgumentException($"Racer with ID {bookingDTO.RacerId} does not exist.");

            // Map DTO to Entity
            var booking = new RaceBooking
            {
                RacerId = bookingDTO.RacerId,
                RaceId = bookingDTO.RaceId,
                BookingType = Enum.TryParse(bookingDTO.BookingType, out BookingType bookingType) ? bookingType : throw new ArgumentException("Invalid Booking Type."),
                AmountPaid = bookingDTO.AmountPaid,
                BookingDate = bookingDTO.BookingDate,
                Racer = racer,
                Game = game
            };

            // Add the booking to the repository
            _raceBookingRepository.AddRaceBooking(booking);

            // Return the created RaceBooking as a DTO
            return new RaceBookingDTO
            {
                BookingId = booking.BookingId,
                RacerId = booking.RacerId,
                RaceId = booking.RaceId,
                BookingType = booking.BookingType.ToString(),
                AmountPaid = booking.AmountPaid,
                BookingDate = booking.BookingDate
            };
        }

        // Get all Race Bookings
        public async Task<IEnumerable<RaceBookingDTO>> GetRaceBookingsAsync()
        {
            var bookings = _raceBookingRepository.GetAllRaceBookings();

            // Map entities to DTOs
            return bookings.Select(b => new RaceBookingDTO
            {
                BookingId = b.BookingId,
                RacerId = b.RacerId,
                RaceId = b.RaceId,
                BookingType = b.BookingType.ToString(),
                AmountPaid = b.AmountPaid,
                BookingDate = b.BookingDate
            });
        }

        // Get a Race Booking by ID
        public async Task<RaceBookingDTO> GetRaceBookingByIdAsync(int bookingId)
        {
            var booking = _raceBookingRepository.GetRaceBookingById(bookingId);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {bookingId} not found.");

            return new RaceBookingDTO
            {
                BookingId = booking.BookingId,
                RacerId = booking.RacerId,
                RaceId = booking.RaceId,
                BookingType = booking.BookingType.ToString(),
                AmountPaid = booking.AmountPaid,
                BookingDate = booking.BookingDate
            };
        }
    }

}
