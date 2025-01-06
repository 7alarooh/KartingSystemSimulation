using Azure;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Authorization; // Added for role-based authorization
using Microsoft.AspNetCore.Mvc;


namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/racers")]
    public class RacerController : ControllerBase
    {
        private readonly IRacerService _racerService;
        private readonly IEmailService _emailService;
        private readonly IMembershipService _membershipService;
        public RacerController(IRacerService racerService, IEmailService emailService, IMembershipService membershipService)
        {
            _racerService = racerService; // Initialize racer service
            _emailService = emailService;
            _membershipService = membershipService;
        }

        /// <summary>
        /// Add a new racer.
        /// - Admins: Can add any racer.
        /// - Supervisors: Can add racers aged between 6 and 18.
        /// - Racers: Can add themselves if they are over 18 years old.
        /// </summary>
        /// <param name="racerInput">Racer input data</param>
        /// <returns>Status message</returns>
        [Authorize(Roles = "Admin,Supervisor,Racer")] // Allow Admins, Supervisors, and Racers
        [HttpPost("AddRacer")]

        public IActionResult AddRacer([FromBody] RacerInputDTO racerInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = "ValidationError", ErrorMessage = "Invalid racer data." });
            }

            try
            {
                // Get the current user's role from the JWT token
                var currentUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(currentUserRole))
                {
                    return Unauthorized("Unauthorized: Unable to retrieve user details.");
                }

                // Calculate the racer's age
                var racerAge = DateTime.Now.Year - racerInput.DOB.Year;
                if (racerInput.DOB.Date > DateTime.Now.AddYears(-racerAge)) racerAge--;

                // Role-based validation
                if (currentUserRole == "Supervisor" && (racerAge < 6 || racerAge > 18))
                {
                    return Forbid("Supervisors can only add racers aged between 6 and 18.");
                }

                if (currentUserRole == "Racer" && racerAge <= 18)
                {
                    return Forbid("Racers can only add themselves if they are over 18 years old.");
                }

                // Admins have no restrictions
                _racerService.AddRacer(racerInput);

                // Return success response
                return Ok(new { message = "Racer added successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { ErrorCode = "DuplicateEntry", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Get a racer by ID. 
        /// - Admins can view any racer's details.
        /// - Racers can view only their own details.
        /// - Supervisors can view details of related racers.
        /// </summary>
        /// <param name="id">Racer ID</param>
        /// <returns>Racer details</returns>
        [HttpGet("{id}")]
        public IActionResult GetRacerById(int id)
        {
            try
            {
                // Get the current user's email and role
                var currentUserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                var currentUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(currentUserEmail) || string.IsNullOrEmpty(currentUserRole))
                {
                    return Unauthorized("Unauthorized: Unable to retrieve user details.");
                }

                // Get the racer's details
                var racer = _racerService.GetRacerById(id);
                if (racer == null)
                {
                    return NotFound(new { ErrorCode = "NotFound", ErrorMessage = "Racer not found." });
                }

                // Allow admins to view any racer's details
                if (currentUserRole == "Admin")
                {
                    return Ok(racer);
                }

                // Allow racers to view their own details
                if (currentUserRole == "Racer" && racer.Email.Equals(currentUserEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return Ok(racer);
                }

                // Allow supervisors to view related racers
                if (currentUserRole == "Supervisor" && _racerService.IsSupervisorRelatedToRacer(currentUserEmail, id))
                {
                    return Ok(racer);
                }

                // Deny access if none of the conditions are met
                return Forbid("Access denied: You do not have permission to view this racer's details.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message });
            }
        }

        // Get all racers (Admin only)
        [Authorize(Roles = "Admin")] // Restrict access to Admins only
        [HttpGet]
        public IActionResult GetAllRacers()
        {
            var racers = _racerService.GetAllRacers();
            return Ok(racers);
        }

        // Update racer (Admin only)
        [Authorize(Roles = "Admin")] // Restrict access to Admins only
        [HttpPut("{id}")]
        public IActionResult UpdateRacer(int id, [FromBody] RacerInputDTO racerInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                _racerService.UpdateRacer(id, racerInput);
                return Ok("Racer updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message });
            }
        }

        // Delete racer (Admin only)
        [Authorize(Roles = "Admin")] // Restrict access to Admins only
        [HttpDelete("{id}")]
        public IActionResult DeleteRacer(int id)
        {
            try
            {
                _racerService.DeleteRacer(id);
                return Ok("Racer deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message });
            }
        }

    }
}
