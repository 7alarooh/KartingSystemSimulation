using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Authorization; // Required for role-based authorization
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        /// Accessible only to users with the Admin role.
        /// </summary>
        /// <param name="adminDto">Admin input DTO</param>
        /// <returns>ActionResult indicating success or failure</returns>
        [HttpPost("admin")]
        public IActionResult RegisterAdmin([FromBody] AdminInputDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                string token = JwtHelper.ExtractToken(Request);
                var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
                var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

                // Validate role
                if (role != "Admin")
                    return Forbid("Only Admins can access this resource.");
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