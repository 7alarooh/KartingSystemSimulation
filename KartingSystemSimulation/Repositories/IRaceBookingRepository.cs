using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceBookingRepository
    {
        void AddRaceBooking(RaceBooking booking);
        void DeleteRaceBooking(RaceBooking booking);
        IEnumerable<RaceBooking> GetAllRaceBookings();
        RaceBooking GetRaceBookingById(int bookingId);
        void UpdateRaceBooking(RaceBooking booking);
    }
}