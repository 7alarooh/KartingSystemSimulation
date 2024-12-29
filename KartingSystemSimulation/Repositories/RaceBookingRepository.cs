using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceBookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RaceBooking> GetAll() => _context.RaceBookings.ToList();
        public RaceBooking GetById(int bookingId) => _context.RaceBookings.Find(bookingId);
        public void Add(RaceBooking booking)
        {
            _context.RaceBookings.Add(booking);
            _context.SaveChanges();
        }
        public void Update(RaceBooking booking)
        {
            _context.RaceBookings.Update(booking);
            _context.SaveChanges();
        }
        public void Delete(RaceBooking booking)
        {
            _context.RaceBookings.Remove(booking);
            _context.SaveChanges();
        }
    }

}
