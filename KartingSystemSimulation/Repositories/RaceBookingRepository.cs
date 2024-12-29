﻿using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public class RaceBookingRepository : IRaceBookingRepository
    {
        private readonly ApplicationDbContext _context;// Database context for accessing the database


        public RaceBookingRepository(ApplicationDbContext context)// Constructor to initialize the repository with the database context
        {   
            _context = context;
        }

        public IEnumerable<RaceBooking> GetAll() => _context.RaceBookings.ToList();
        public RaceBooking GetById(int bookingId) => _context.RaceBookings.Find(bookingId);
        public void Add(RaceBooking booking)// Adds a new RaceBooking entity to the database
        {
            _context.RaceBookings.Add(booking);
            _context.SaveChanges();
        }
        public void Update(RaceBooking booking)// Updates an existing RaceBooking entity in the database
        {
            _context.RaceBookings.Update(booking);
            _context.SaveChanges();
        }
        public void Delete(RaceBooking booking)// Deletes a Booking entity from the database
        {
            _context.RaceBookings.Remove(booking);
            _context.SaveChanges();
        }
    }

}
