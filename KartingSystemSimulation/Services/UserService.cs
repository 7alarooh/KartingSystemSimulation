using AutoMapper;
using KartingSystemSimulation.Controllers;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KartingSystemSimulation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IAdminRepository adminRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
            _config = config;
        }

        // Fetch all users and map to DTO
        public IEnumerable<UserOutputDTO> GetAll()
        {
            var users = _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserOutputDTO>>(users);
        }

        // Fetch a specific user by ID and map to DTO
        public UserOutputDTO GetById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");
            return _mapper.Map<UserOutputDTO>(user);
        }

        // Add a new user to the system
        public void AddUser(UserInputDTO userDto)
        {
            if (!IsValidEmail(userDto.LoginEmail)) // Validate email format
                throw new ArgumentException("Invalid email format.");

            if (!IsValidPassword(userDto.Password)) // Validate password strength
                throw new ArgumentException("Password must be at least 8 characters.");

            var user = _mapper.Map<User>(userDto);
            user.LoginPassword = HashPassword(userDto.Password); // Hash password before saving
            _userRepository.AddUser(user);
        }

        // Add user for testing purposes only
        public User TestAddUser(UserInputDTO userInputDTO)
        {
            if (!IsValidEmail(userInputDTO.LoginEmail))
                throw new ArgumentException("Invalid email format.");

            var user = _mapper.Map<User>(userInputDTO);
            user.LoginPassword = HashPassword(userInputDTO.Password); // Hash the password
            _userRepository.AddUser(user);

            return user;
        }

        // Update an existing user's details
        public void Update(int userId, UserInputDTO userDto)
        {
            var existingUser = _userRepository.GetUserById(userId);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            if (!IsValidEmail(userDto.LoginEmail))
                throw new ArgumentException("Invalid email format.");

            _mapper.Map(userDto, existingUser);
            _userRepository.UpdateUser(existingUser);
        }

        // Delete a user with admin authorization
        public void Delete(int userId, string adminEmail)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var admin = _adminRepository.GetAllAdmins().FirstOrDefault(a => a.Email == adminEmail);
            if (admin == null)
                throw new UnauthorizedAccessException("Only an admin can delete a user.");

            _userRepository.DeleteUser(user);
        }

        // Authenticate a user and generate JWT token
        public string AuthenticateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null || !VerifyPassword(password, user.LoginPassword)) // Validate email and password
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var role = Enum.Parse<Role>(user.Role.ToString()); // Fetch user role
            var permissions = GetUserPermissions(role); // Fetch permissions for the role

            return GenerateJwtToken(user.LoginEmail, role.ToString(), permissions); // Generate JWT token
        }

        // Generate a JWT token with claims

        private string GenerateJwtToken(string email, string role, string permissions)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("JWT secret key is not configured.");
            }
            // Define claims for the token
            var claims = new List<Claim>
                       {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Permissions", permissions) // Custom permissions
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token); // Return token string
        }
 
        // Verify if the provided password matches the stored hashed password
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            return HashPassword(enteredPassword) == storedHashedPassword;
        }

        // Get permissions based on user role
        private string GetUserPermissions(Role role)
        {
            return role switch
            {
                Role.Admin => "ManageUsers,EditConfig",
                Role.Racer => "ParticipateRace,ViewLeaderboard",
                Role.Supervisor => "ManageRacers,ApproveRaces",
                _ => string.Empty
            };
        }

        // Validate if the email format is correct
        private bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }

        // Hash the password using SHA256
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        // Check if the password meets minimum strength requirements
        private bool IsValidPassword(string password)
        {
            return password.Length >= 8; // Example: Ensure password has at least 8 characters
        }
    }
}
