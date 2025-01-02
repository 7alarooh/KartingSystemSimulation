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
                // Step 1: Validate email format
                if (!IsValidEmail(adminDto.Email))
                {
                    throw new ArgumentException("Invalid email format.");
                }

                // Step 2: Check if email already exists in the Users table
                var existingUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.LoginEmail == adminDto.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("User with the same email already exists.");
                }

                // Step 3: Hash the default password
                var hashedPassword = HashPassword("DefaultPassword123");

                // Step 4: Add the user to the Users table
                var user = new User
                {
                    LoginEmail = adminDto.Email, // Use the admin's email as the login email
                    LoginPassword = hashedPassword, // Save hashed password
                    Role = Role.Admin // Assign the Admin role
                };
                _userRepository.AddUser(user);

                // Step 5: Map and add the admin to the Admins table
                var admin = new Admin
                {
                    FirstName = adminDto.FirstName,
                    LastName = adminDto.LastName,
                    Phone = adminDto.Phone,
                    CivilId = adminDto.CivilId,
                    Email = user.LoginEmail,
                    Gender = adminDto.Gender,
                    Address = adminDto.Address,
                };
                _adminRepository.AddAdmin(admin);

                // Step 6: Commit the transaction to save both records
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Rollback on error to maintain atomicity
                throw new InvalidOperationException("Error during admin registration.", ex);
            }
        }

        public void RegisterSupervisor(SupervisorInputDTO supervisorDto)
        {
            using var transaction = _context.Database.BeginTransaction(); // Begin a transaction to ensure atomicity.
            try
            {
                // Validate email format
                if (!IsValidEmail(supervisorDto.Email))
                    throw new ArgumentException("Invalid email format.", nameof(supervisorDto.Email));

                // Ensure the Civil ID is unique
                if (_context.Supervisors.Any(s => s.CivilId == supervisorDto.CivilId))
                    throw new InvalidOperationException("A supervisor with the same Civil ID already exists.");

                // Ensure the Phone number is unique
                if (_context.Supervisors.Any(s => s.Phone == supervisorDto.Phone))
                    throw new InvalidOperationException("A supervisor with the same Phone number already exists.");

                // Hash the password
                var hashedPassword = HashPassword("DefaultPassword123");

                // Add the supervisor to the Supervisors table
                var supervisor = new Supervisor
                {
                    Name = supervisorDto.Name,
                    CivilId = supervisorDto.CivilId,
                    Phone = supervisorDto.Phone,
                    Email = supervisorDto.Email
                };
                _context.Supervisors.Add(supervisor);

                // Add the user to the Users table
                var user = new User
                {
                    LoginEmail = supervisorDto.Email,
                    Role = Role.Supervisor, // Set the role as Supervisor
                    LoginPassword = hashedPassword // Store the hashed password
                };
                _context.Users.Add(user);

                // Save changes and commit the transaction
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Rollback the transaction if an error occurs
                throw new InvalidOperationException("Failed to register supervisor. See inner exception for details.", ex);
            }
        }


        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
        private bool IsValidPassword(string password)
        {
            return password.Length >= 8; // Example: Ensure password has at least 8 characters
        }
        private bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }
    }
}