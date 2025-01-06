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
            try
            {
                string token = JwtHelper.ExtractToken(Request);
                var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
                var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

                // Validate role
                if (role != "Admin")
                    return Forbid("Only Admins can access this resource.");

                // Optional: Check permissions
                if (!JwtHelper.HasPermission(token, "ManageUsers"))
                    return Forbid("You do not have the required permission.");

                var users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }
        }


        /// <summary>
        /// Get a user by ID (Admin only).
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>UserOutputDTO</returns>
        [Authorize(Roles = "Admin")]
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
        /// Add a new user (Admin only).
        /// </summary>
        /// <param name="userDto">User input DTO</param>
        /// <returns>Status message</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddUser([FromBody] UserInputDTO userDto)
        {
            // Validate input data
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = "Invalid input data." });
            }

            try
            {
                // Call the service layer to add the user
                _userService.TestAddUser(userDto);

                // Return success response
                return CreatedAtAction(nameof(GetUserById), new { id = userDto.LoginEmail }, new { message = "User added successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new { ErrorCode = ErrorCode.UnknownError.ToString(), ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// Update an existing user (Admin only).
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="userDto">Updated user data</param>
        /// <returns>Status message</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserInputDTO userDto)
        {
            // Validate the input data
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = "Invalid input data." });
            }

            try
            {
                // Call the service layer to update the user
                _userService.Update(id, userDto);

                // Return a success response
                return Ok(new { message = "User updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                // Handle the case where the user ID does not exist
                return NotFound(new { ErrorCode = ErrorCode.NotFound.ToString(), ErrorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Handle validation or business logic errors
                return BadRequest(new { ErrorCode = ErrorCode.ValidationError.ToString(), ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }


        /// <summary>
        /// Delete a user (Admin only).
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Status message</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.Delete(id, User.FindFirstValue(ClaimTypes.Email));
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
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                // Call the service to authenticate the user using email and password
                // This method validates the user's credentials and generates a JWT token if valid
                var user = _userService.AuthenticateUser(loginDto.Email, loginDto.Password);

                // Ensure the user exists and is authenticated
                if (user != null)
                {
                    // Return the authenticated user's details, including the JWT token
                    return Ok(user);
                }

                // If authentication fails, return an Unauthorized response
                return Unauthorized(new { Message = "Invalid email or password." });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle cases where the user is not authorized due to invalid credentials
                return Unauthorized(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors that may occur during authentication
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

        [NonAction]
        public string GenerateJwtToken(string email, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Email, email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
