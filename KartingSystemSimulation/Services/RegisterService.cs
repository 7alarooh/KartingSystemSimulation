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
        private readonly IEmailService _emailService;

        public RegisterService(IAdminRepository adminRepository, IUserRepository userRepository, ApplicationDbContext context, IEmailService emailService)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _context = context;
            _emailService = emailService;
        }
        public void RegisterAdmin(AdminInputDTO adminDto)
        {
            using var transaction = _context.Database.BeginTransaction();            // Begin transaction
            try
            {
                // Step 1: Validate email
                if (!IsValidEmail(adminDto.Email))
                {
                    throw new ArgumentException("Invalid email format.");
                }

                // Step 2: Check for duplicates
                if (_userRepository.GetAllUsers().Any(u => u.LoginEmail == adminDto.Email))
                {
                    throw new InvalidOperationException("User with this email already exists.");
                }

                // Step 3: Hash password and save user
                var hashedPassword = HashPassword("DefaultPassword123");
                var user = new User
                {
                    LoginEmail = adminDto.Email,
                    LoginPassword = hashedPassword,
                    Role = Role.Admin
                };
                _userRepository.AddUser(user);

                // Step 4: Save admin
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

                _context.SaveChanges(); // Save all changes
                transaction.Commit(); // Commit transaction

                // Step 5: Send email
                string subject = "Welcome to Karting System - Admin Registration";
                string body = $@"
            <h3>Dear {admin.FirstName} {admin.LastName},</h3>
            <p>You have been successfully registered as an Admin in Karting System.</p>
            <ul>
                <li>Email: {admin.Email}</li>
                <li>Password: DefaultPassword123</li>
            </ul>
            <p><strong>Note:</strong> Please change your password after your first login.</p>";
                _emailService.SendEmailAsync(admin.Email, subject, body).Wait();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new InvalidOperationException("Error during admin registration.", ex);
            }
        }



        public void RegisterSupervisor(SupervisorInputDTO supervisorDto)
        {
            using var transaction = _context.Database.BeginTransaction(); // Begin transaction
            try
            {
                // Validate input
                if (!IsValidEmail(supervisorDto.Email))
                {
                    throw new ArgumentException("Invalid email format.");
                }

                if (_context.Supervisors.Any(s => s.CivilId == supervisorDto.CivilId))
                {
                    throw new InvalidOperationException("Supervisor with this Civil ID already exists.");
                }

                // Save supervisor
                var hashedPassword = HashPassword("DefaultPassword123");
                var supervisor = new Supervisor
                {
                    Name = supervisorDto.Name,
                    CivilId = supervisorDto.CivilId,
                    Phone = supervisorDto.Phone,
                    Email = supervisorDto.Email
                };
                _context.Supervisors.Add(supervisor);

                // Save user
                var user = new User
                {
                    LoginEmail = supervisorDto.Email,
                    LoginPassword = hashedPassword,
                    Role = Role.Supervisor
                };
                _context.Users.Add(user);

                _context.SaveChanges();
                transaction.Commit();

                // Send email
                string subject = "Welcome to Karting System - Supervisor Registration";
                string body = $@"
            <h3>Dear {supervisor.Name},</h3>
            <p>You have been successfully registered as a Supervisor in Karting System.</p>
            <ul>
                <li>Email: {supervisor.Email}</li>
                <li>Password: DefaultPassword123</li>
            </ul>
            <p><strong>Note:</strong> Please change your password after your first login.</p>";
                _emailService.SendEmailAsync(supervisor.Email, subject, body).Wait();
    }
    catch (Exception ex)
    {
        transaction.Rollback();
        throw new InvalidOperationException("Error registering supervisor.", ex);
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