using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RacerInputDTO
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
        public string FirstName { get; set; } // Racer's first name

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
        public string LastName { get; set; } // Racer's last name

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } // Racer's contact number

        [Required(ErrorMessage = "CivilId is required.")]
        public string CivilId { get; set; } // Unique Civil ID for identification

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } // Racer's validated email address

        [Required(ErrorMessage = "DOB is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DOB { get; set; } // Racer's date of birth

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("(Male|Female)", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public Gender Gender { get; set; } // Racer's gender

        [Required(ErrorMessage = "State is required.")]
        public Address Address { get; set; } // Racer's state of residence

        [Required(ErrorMessage = "AgreedToRules is required.")]
        public bool AgreedToRules { get; set; } // Indicates agreement to privacy and rules

        public int? SupervisorId { get; set; } // Optional supervisor ID for underage racers
    }
}
