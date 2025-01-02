using KartingSystemSimulation.DTOs;

namespace KartingSystemSimulation.Services
{
    public interface IRaceBookingService
    {
        Task<RaceBookingDTO> CreateRaceBookingAsync(RaceBookingDTO bookingDTO);
        Task<RaceBookingDTO> GetRaceBookingByIdAsync(int bookingId);
        Task<IEnumerable<RaceBookingDTO>> GetRaceBookingsAsync();
    }
}