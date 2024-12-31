using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAdminRepository adminRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserOutputDTO> GetAll()
        {
            var users = _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserOutputDTO>>(users);
        }

        public UserOutputDTO GetById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");
            return _mapper.Map<UserOutputDTO>(user);
        }

        public void AddUser(UserInputDTO userDto)
        {
            if (!IsValidEmail(userDto.LoginEmail))
                throw new ArgumentException("Invalid email format.");

            if (!IsValidPassword(userDto.Password))
                throw new ArgumentException("Password must be at least 8 characters.");

            var user = _mapper.Map<User>(userDto);
            user.LoginPassword = HashPassword(userDto.Password); // Hash password before saving
            _userRepository.AddUser(user);
        }

<<<<<<< HEAD

=======
        // Added For Testing only
        public User TestAddUser (UserInputDTO userInputDTO)
        {
            if (!IsValidEmail(userInputDTO.LoginEmail))
                throw new ArgumentException("Invalid email format.");

            var user = _mapper.Map<User>(userInputDTO);
            user.LoginPassword = HashPassword(userInputDTO.Password); // Hash the password
            _userRepository.AddUser(user);

            return user;
        }
>>>>>>> c5dce455488c365d56422c4c8d833be9f1d3b167

        // Added For Testing only
        public User TestAddUser (UserInputDTO userInputDTO)
        {
            if (!IsValidEmail(userInputDTO.LoginEmail))
                throw new ArgumentException("Invalid email format.");

            var user = _mapper.Map<User>(userInputDTO);
            user.LoginPassword = HashPassword(userInputDTO.Password); // Hash the password
            _userRepository.AddUser(user);

            return user;
        }

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

        // Helper methods for validation and hashing
        private bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
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
    }
}
