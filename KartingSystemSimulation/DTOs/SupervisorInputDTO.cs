using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class SupervisorInputDTO
    {
        [Required(ErrorMessage = "SupervisorId is required.")]
        public int SupervisorId { get; set; } // Primary key

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } // Supervisor's name

        [Required(ErrorMessage = "CivilId is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "CivilId must be between 8 and 20 characters.")]
        public string CivilId { get; set; } // Unique Civil ID

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } // Contact number

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } // Validated email
    }
}
