using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    /// <summary>
    /// Service layer for managing Supervisor-related operations.
    /// Implements business logic and validations.
    /// </summary>
    public class SupervisorService : ISupervisorService
    {
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to inject the Supervisor repository dependency.
        /// </summary>
        /// <param name="supervisorRepository">Repository to handle data access for Supervisor entities.</param>
        public SupervisorService(ISupervisorRepository supervisorRepository, IMapper mapper)
        {
            _supervisorRepository = supervisorRepository; // Inject the repository
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all supervisors from the database.
        /// </summary>
        /// <returns>A list of all supervisors.</returns>
        public IEnumerable<Supervisor> GetAllSupervisors()
        {
            // Fetch all supervisors from the repository
            return _supervisorRepository.GetAllSupervisors();
        }

        /// <summary>
        /// Retrieves a specific supervisor by their ID.
        /// </summary>
        /// <param name="supervisorId">The ID of the supervisor to retrieve.</param>
        /// <returns>The supervisor entity.</returns>
        /// <exception cref="ArgumentException">Thrown if the ID is invalid.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the supervisor is not found.</exception>
        public Supervisor GetSupervisorById(int supervisorId)
        {
            if (supervisorId <= 0) // Validate input
                throw new ArgumentException("Supervisor ID must be greater than 0.", nameof(supervisorId));

            var supervisor = _supervisorRepository.GetSupervisorById(supervisorId);

            if (supervisor == null) // Handle not found case
                throw new KeyNotFoundException($"Supervisor with ID {supervisorId} not found.");

            return supervisor;
        }

        /// <summary>
        /// Adds a new supervisor to the database.
        /// Validates input data before saving.
        /// </summary>
        /// <param name="supervisor">The supervisor entity to add.</param>
        /// <exception cref="ArgumentException">Thrown if input validation fails.</exception>
        public Supervisor AddSupervisor(SupervisorInputDTO supervisorInput)
        {
            // Input validation for supervisor entity
            if (string.IsNullOrWhiteSpace(supervisorInput.Name))
                throw new ArgumentException("Supervisor name cannot be empty.");

            if (string.IsNullOrWhiteSpace(supervisorInput.Email) || !IsValidEmail(supervisorInput.Email))
                throw new ArgumentException("Invalid email format.");

            if (string.IsNullOrWhiteSpace(supervisorInput.Phone) || !IsValidPhone(supervisorInput.Phone))
                throw new ArgumentException("Invalid phone number.");

            // Call repository to add the supervisor
            var supervisor = _supervisorRepository.AddSupervisor(_mapper.Map<Supervisor>(supervisorInput));

            return supervisor;
        }

        /// <summary>
        /// Updates an existing supervisor in the database.
        /// Validates the input and ensures the supervisor exists.
        /// </summary>
        /// <param name="supervisor">The updated supervisor entity.</param>
        /// <exception cref="ArgumentException">Thrown if input validation fails.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the supervisor does not exist.</exception>
        public void UpdateSupervisor(Supervisor supervisor)
        {
            if (supervisor.SupervisorId <= 0) // Validate ID
                throw new ArgumentException("Supervisor ID must be greater than 0.", nameof(supervisor.SupervisorId));

            // Check if the supervisor exists in the database
            var existingSupervisor = _supervisorRepository.GetSupervisorById(supervisor.SupervisorId);
            if (existingSupervisor == null)
                throw new KeyNotFoundException($"Supervisor with ID {supervisor.SupervisorId} not found.");

            // Input validation
            if (string.IsNullOrWhiteSpace(supervisor.Name))
                throw new ArgumentException("Supervisor name cannot be empty.");

            if (string.IsNullOrWhiteSpace(supervisor.Email) || !IsValidEmail(supervisor.Email))
                throw new ArgumentException("Invalid email format.");

            if (string.IsNullOrWhiteSpace(supervisor.Phone) || !IsValidPhone(supervisor.Phone))
                throw new ArgumentException("Invalid phone number.");

            // Call repository to update the supervisor
            _supervisorRepository.UpdateSupervisor(supervisor);
        }

        /// <summary>
        /// Deletes a supervisor from the database.
        /// Ensures the supervisor exists before deletion.
        /// </summary>
        /// <param name="supervisorId">The ID of the supervisor to delete.</param>
        /// <exception cref="ArgumentException">Thrown if the ID is invalid.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the supervisor does not exist.</exception>
        public void DeleteSupervisor(int supervisorId)
        {
            if (supervisorId <= 0) // Validate ID
                throw new ArgumentException("Supervisor ID must be greater than 0.", nameof(supervisorId));

            // Check if the supervisor exists
            var supervisor = _supervisorRepository.GetSupervisorById(supervisorId);
            if (supervisor == null)
                throw new KeyNotFoundException($"Supervisor with ID {supervisorId} not found.");

            // Call repository to delete the supervisor
            _supervisorRepository.DeleteSupervisor(supervisor);
        }

        /// <summary>
        /// Validates an email address format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates a phone number format.
        /// Ensures it contains only digits and has a valid length.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private bool IsValidPhone(string phone)
        {
            // Example: Validate phone number (only digits, 8-15 length)
            return phone.All(char.IsDigit) && phone.Length >= 8 && phone.Length <= 15;
        }
    }
}
