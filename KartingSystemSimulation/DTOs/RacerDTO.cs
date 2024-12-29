using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RacerDTO
    {
        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // Primary key

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } // Racer's first name

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; } // Racer's last name

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } // Contact number

        [Required(ErrorMessage = "CivilId is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "CivilId must be between 8 and 20 characters.")]
        public string CivilId { get; set; } // Unique Civil ID

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } // Validated email

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2005", ErrorMessage = "Date of Birth must be between 01/01/1900 and 01/01/2005.")]
        public DateTime DOB { get; set; } // Date of birth

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender must be either Male, Female, or Other.")]
        public string Gender { get; set; } // Gender

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot be longer than 50 characters.")]
        public string State { get; set; } // State of residence

        [Required(ErrorMessage = "Membership is required.")]
        [StringLength(10, ErrorMessage = "Membership cannot be longer than 10 characters.")]
        [RegularExpression(@"^(Gold|Diamond|Normal)$", ErrorMessage = "Membership must be either Gold, Diamond, or Normal.")]
        public string Membership { get; set; } // Membership type (Gold/Diamond/Normal)
    }
}
