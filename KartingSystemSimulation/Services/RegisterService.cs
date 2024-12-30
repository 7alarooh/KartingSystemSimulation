using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public RegisterService(IAdminRepository adminRepository, IUserRepository userRepository, ApplicationDbContext context)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _context = context;
        }

        public void RegisterAdmin(AdminInputDTO adminDto)
        {
            using var transaction = _context.Database.BeginTransaction(); // Start transaction
            try
            {
                // Validate email
                if (!IsValidEmail(adminDto.Email))
                {
                    throw new ArgumentException("Invalid email format.");
                }

                // Check if the email exists in the Users table
                var existingUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.LoginEmail == adminDto.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("User with the same email already exists.");
                }

                // Hash the password
                var hashedPassword = HashPassword("DefaultPassword123");

                // Add the user to the Users table
                var user = new User
                {
                    LoginEmail = adminDto.Email,
                    LoginPassword = hashedPassword,
                    Role = Role.Admin
                };
                _userRepository.AddUser(user);

                // Map and add the admin to the Admins table
                var admin = new Admin
                {
                    FirstName = adminDto.FirstName,
                    LastName = adminDto.LastName,
                    Phone = adminDto.Phone,
                    CivilId = adminDto.CivilId,
                    Email = adminDto.Email,
                    Gender = adminDto.Gender,
                    Address = adminDto.Address
                };
                _adminRepository.AddAdmin(admin);

                transaction.Commit(); // Commit the transaction
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Rollback transaction on error
                throw new InvalidOperationException("Error during admin registration.", ex);
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }
    }
}
