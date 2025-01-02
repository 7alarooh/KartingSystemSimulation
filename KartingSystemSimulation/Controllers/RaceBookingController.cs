using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceBookingController : ControllerBase
    {
        private readonly IRaceBookingService _raceBookingService;

        public RaceBookingController(IRaceBookingService raceBookingService)
        {
            _raceBookingService = raceBookingService;
        }

        // Endpoint to create a Race Booking
        [HttpPost("create")]
        public async Task<IActionResult> CreateRaceBooking(RaceBookingDTO bookingDTO)
        {
            try
            {
                var result = await _raceBookingService.CreateRaceBookingAsync(bookingDTO);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        // Endpoint to get all Race Bookings
        [HttpGet("list")]
        public async Task<IActionResult> GetRaceBookings()
        {
            try
            {
                var result = await _raceBookingService.GetRaceBookingsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        // Endpoint to get a Race Booking by ID
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetRaceBookingById(int bookingId)
        {
            try
            {
                var result = await _raceBookingService.GetRaceBookingByIdAsync(bookingId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }

}
