using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        private readonly IEmailService _emailService;
        public RegisterController(IRegisterService registerService, IMapper mapper, IEmailService emailService)
        {
            _registerService = registerService;
            _mapper = mapper;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a new admin and adds to both Admin and User tables.
        /// </summary>
        /// <param name="adminDto">Admin input DTO</param>
        /// <returns>ActionResult indicating success or failure</returns>
        [HttpPost("admin")]
        public IActionResult RegisterAdmin([FromBody] AdminInputDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors.
            }

            try
            {
                // Call the service to register the admin
                _registerService.RegisterAdmin(adminDto);
                return CreatedAtAction(nameof(RegisterAdmin), new { email = adminDto.Email }, adminDto); // Success response
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { ErrorCode = "DuplicateEntry", ErrorMessage = ex.Message }); // Conflict error
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message }); // Generic server error
            }
        }


        /// <summary>
        /// Registers a new supervisor and adds to both Supervisor and User tables.
        /// </summary>
        /// <param name="supervisorDto">Supervisor input DTO</param>
        /// <returns>ActionResult indicating success or failure</returns>
        [HttpPost("supervisor")]
        public IActionResult RegisterSupervisor([FromBody] SupervisorInputDTO supervisorDto, IEmailService emailService)
        {
            try
            {
                // Validate the input
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the RegisterService to register the supervisor
                _registerService.RegisterSupervisor(supervisorDto);

                return CreatedAtAction(nameof(RegisterSupervisor), new { email = supervisorDto.Email }, supervisorDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    ErrorCode = "DuplicateEntry",
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ErrorCode = "UnknownError",
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}