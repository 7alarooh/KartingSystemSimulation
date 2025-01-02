using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KartingSystemSimulation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService; // Inject UserService
            _configuration = configuration;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of UserOutputDTO</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>UserOutputDTO</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="userDto">User input DTO</param>
        /// <returns>Status message</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] UserInputDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = "Invalid input data." });
            }

            try
            {
                _userService.TestAddUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = userDto.LoginEmail }, new { message = "User added successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = ErrorCode.UnknownError.ToString(), ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="userDto">Updated user data</param>
        /// <returns>Status message</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserInputDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = "Invalid input data." });
            }

            try
            {
                _userService.Update(id, userDto);
                return Ok(new { message = "User updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="adminEmail">Admin's email for authorization</param>
        /// <returns>Status message</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id, [FromQuery] string adminEmail)
        {
            try
            {
                _userService.Delete(id, adminEmail);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ErrorCode = ErrorCode.OperationNotAllowed.ToString(), ErrorMessage = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var user = _userService.AuthenticateUser(loginDto.Email, loginDto.Password);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ErrorMessage = ex.Message });
            }
        }

        [NonAction]
        public string GenerateJwtToken(string userId, string username, string role, string email, string permissions)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("JWT secret key is not configured.");
            }

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId),
        new Claim(JwtRegisteredClaimNames.UniqueName, username),
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Email, email),
        new Claim("Permissions", permissions) // Add permissions as a custom claim
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
