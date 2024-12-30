using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

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

        // Get all supervisors
        [HttpGet]
        public IActionResult GetAllSupervisors()
        {
            // Fetch all supervisors from the service layer
            var supervisors = _supervisorService.GetAllSupervisors();

            // Convert domain models to DTOs for client response
            var result = _mapper.Map<IEnumerable<SupervisorOutputDTO>>(supervisors);

            return Ok(result); // Return HTTP 200 with the list of supervisors
        }

        // Get a supervisor by ID
        [HttpGet("{id}")]
        public IActionResult GetSupervisorById(int id)
        {
            try
            {
                // Fetch a specific supervisor by ID
                var supervisor = _supervisorService.GetSupervisorById(id);

                // Convert the domain model to DTO for client response
                var result = _mapper.Map<SupervisorOutputDTO>(supervisor);

                return Ok(result); // Return HTTP 200 with the supervisor data
            }
            catch (KeyNotFoundException ex)
            {
                // Handle the case where the supervisor is not found
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Handle invalid input errors
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = ex.Message });
            }
        }

        // Update an existing supervisor
        [HttpPut("{id}")]
        public IActionResult UpdateSupervisor(int id, [FromBody] SupervisorOutputDTO supervisorDto)
        {
            // Check if the input data is valid
            if (!ModelState.IsValid)
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = "Invalid supervisor data." });

            try
            {
                // Map the DTO to the domain model
                var supervisor = _mapper.Map<Supervisor>(supervisorDto);
                supervisor.SupervisorId = id; // Ensure the ID is correctly set

                // Update the supervisor through the service layer
                _supervisorService.UpdateSupervisor(supervisor);

                return NoContent(); // Return HTTP 204 for a successful update
            }
            catch (KeyNotFoundException ex)
            {
                // Handle the case where the supervisor is not found
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Handle invalid update operations
                return BadRequest(new { ErrorCode = ErrorCode.OperationNotAllowed.ToString(), ErrorMessage = ex.Message });
            }
        }

        // Delete a supervisor by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteSupervisor(int id)
        {
            try
            {
                // Delete the supervisor through the service layer
                _supervisorService.DeleteSupervisor(id);

                return NoContent(); // Return HTTP 204 for a successful deletion
            }
            catch (KeyNotFoundException ex)
            {
                // Handle the case where the supervisor is not found
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Handle invalid delete operations
                return BadRequest(new { ErrorCode = ErrorCode.OperationNotAllowed.ToString(), ErrorMessage = ex.Message });
            }
        }
    }
}
