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

        public RegisterController(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers a new admin and adds to both Admin and User tables.
        /// </summary>
        /// <param name="adminDto">Admin input DTO</param>
        /// <returns>ActionResult indicating success or failure</returns>
        [HttpPost("admin")]
        public IActionResult RegisterAdmin([FromBody] AdminInputDTO adminDto)
        {
            try
            {
                // Validate the input
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the RegisterService to register the admin using the DTO directly
                _registerService.RegisterAdmin(adminDto);

                return CreatedAtAction(nameof(RegisterAdmin), new { email = adminDto.Email }, adminDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { ErrorCode = "DuplicateEntry", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "UnknownError", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Registers a new supervisor and adds to both Supervisor and User tables.
        /// </summary>
        /// <param name="supervisorDto">Supervisor input DTO</param>
        /// <returns>ActionResult indicating success or failure</returns>
        [HttpPost("supervisor")]
        public IActionResult RegisterSupervisor([FromBody] SupervisorInputDTO supervisorDto)
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