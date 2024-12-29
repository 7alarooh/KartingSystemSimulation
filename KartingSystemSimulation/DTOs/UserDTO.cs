using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; } // Primary key

        [Required(ErrorMessage = "LoginEmail is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string LoginEmail { get; set; } // User's login email

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Admin|Racer|Supervisor)$", ErrorMessage = "Role must be one of the following: Admin, Racer, Supervisor.")]
        public string Role { get; set; } // User's role (Admin, Racer, Supervisor)
    }
}
