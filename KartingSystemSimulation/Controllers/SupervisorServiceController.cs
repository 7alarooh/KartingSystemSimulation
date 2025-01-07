using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Authorization; // For role-based authorization
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;
        private readonly IMapper _mapper;

        // Constructor: Injects the SupervisorService and AutoMapper
        public SupervisorController(ISupervisorService supervisorService, IMapper mapper)
        {
            _supervisorService = supervisorService; // Business logic layer for Supervisors
            _mapper = mapper; // Maps between domain models and DTOs
        }

        /// <summary>
        /// Get all supervisors (Admin only).
        /// </summary>
        /// <returns>List of SupervisorOutputDTO</returns>
      
        [HttpGet]
        public IActionResult GetAllSupervisors()
        {  // Restrict to Admin role only
            string token = JwtHelper.ExtractToken(Request);
            var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
            var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

            // Validate role
            if (role != "Admin")
                return Forbid("Only Admins can access this resource.");
            //
            var supervisors = _supervisorService.GetAllSupervisors();
            var result = _mapper.Map<IEnumerable<SupervisorOutputDTO>>(supervisors);
            return Ok(result);
        }

        /// <summary>
        /// Get a supervisor by ID. Admins can view any supervisor's details; supervisors can view their own details only.
        /// </summary>
        /// <param name="id">Supervisor ID</param>
        /// <returns>Supervisor details</returns>
        [HttpGet("{id}")]
        public IActionResult GetSupervisorById(int id)
        {
            try
            {
                // Get the email and role of the currently logged-in user
                var currentUserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                var currentUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(currentUserEmail) || string.IsNullOrEmpty(currentUserRole))
                {
                    return Unauthorized("Unauthorized: Unable to retrieve user details.");
                }

                // Retrieve the supervisor's details by ID
                var supervisor = _supervisorService.GetSupervisorById(id);

                if (supervisor == null)
                {
                    return NotFound(new { ErrorCode = "NotFound", ErrorMessage = "Supervisor not found." });
                }

                // Check if the user is an admin or the current supervisor
                if (currentUserRole != "Admin" && !supervisor.Email.Equals(currentUserEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid(); // Use Forbid() without parameters
                }

                // Map the supervisor entity to a DTO
                var result = _mapper.Map<SupervisorOutputDTO>(supervisor);

                // Return the supervisor's details
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing supervisor (Admin only).
        /// </summary>
        /// <param name="id">Supervisor ID</param>
        /// <param name="supervisorDto">Supervisor update data</param>
        /// <returns>Status message</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateSupervisor(int id, [FromBody] SupervisorOutputDTO supervisorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = "Invalid supervisor data." });

            try
            {

                // Restrict to Admin role only
                string token = JwtHelper.ExtractToken(Request);
                var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
                var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

                // Validate role
                if (role != "Admin")
                    return Forbid("Only Admins can access this resource.");
                var supervisor = _mapper.Map<Supervisor>(supervisorDto);
                supervisor.SupervisorId = id;
                _supervisorService.UpdateSupervisor(supervisor);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorCode = ErrorCode.OperationNotAllowed.ToString(), ErrorMessage = ex.Message });
            }
        }
        /// <summary>
        /// Delete a supervisor (Admin only).
        /// </summary>
        /// <param name="id">Supervisor ID</param>
        /// <returns>Status message</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteSupervisor(int id)
        {
            try
            {
                string token = JwtHelper.ExtractToken(Request);
                var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
                var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

                // Validate role
                if (role != "Admin")
                    return Forbid("Only Admins can access this resource.");

                _supervisorService.DeleteSupervisor(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorCode = ErrorCode.OperationNotAllowed.ToString(), ErrorMessage = ex.Message });
            }
        }

        //----------------------------------------------
        /// <summary>
        /// Add a new supervisor (Admin only).
        /// </summary>
        /// <param name="supervisorInput">Supervisor input data</param>
        /// <returns>Supervisor ID</returns>
        [Authorize(Roles = "Admin")] // Restrict to Admin role only
        [HttpPost("AddSupervisor")]
        public IActionResult AddSupervisor([FromBody] SupervisorInputDTO supervisorInput)
        {
            try
            {
                // Call the service layer to add a new supervisor
                // The service returns the newly created supervisor's ID
                var supervisorId = _supervisorService.AddSupervisor(supervisorInput);

                // Return success response with the supervisor's ID
                return Ok(new { SupervisorId = supervisorId, Message = "Supervisor added successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle validation or business logic errors
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new { ErrorCode = ErrorCode.UnknownError.ToString(), ErrorMessage = ex.Message });
            }
        }

    }
}
