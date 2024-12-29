using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceBookingRepository
    {
        void Add(RaceBooking booking);
        void Delete(RaceBooking booking);
        IEnumerable<RaceBooking> GetAll();
        RaceBooking GetById(int bookingId);
        void Update(RaceBooking booking);
    }
}