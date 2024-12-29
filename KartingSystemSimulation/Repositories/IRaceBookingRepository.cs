using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IRaceBookingRepository
    {
        // Add a new RaceBooking record to the database
        void Add(RaceBooking booking);

        // Delete an existing RaceBooking record from the database
        void Delete(RaceBooking booking);

        // Retrieve all RaceBooking records from the database
        IEnumerable<RaceBooking> GetAll();

        // Retrieve a specific RaceBooking record by its unique ID
        RaceBooking GetById(int bookingId);

        // Update an existing RaceBooking record in the database
        void Update(RaceBooking booking);
    }
}
