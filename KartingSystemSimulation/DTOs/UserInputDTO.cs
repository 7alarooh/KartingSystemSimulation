using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class UserInputDTO
    {
        [Required(ErrorMessage = "LoginEmail is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Admin|Racer|Supervisor)$", ErrorMessage = "Role must be one of the following: Admin, Racer, Supervisor.")]
        public string Role { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } // Plain password input
    }
}
