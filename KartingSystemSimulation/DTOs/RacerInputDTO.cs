using KartingSystemSimulation.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RacerInputDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Civil ID is required.")]
        public string CivilId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("(Male|Female)", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "You must agree to the privacy and rules.")]
        public bool AgreedToRules { get; set; }

        public int? SupervisorId { get; set; } // Optional supervisor ID for underage racers

        //[Required(ErrorMessage = "Membership type is required.")]
        [RegularExpression("(Gold|Diamond|Normal)", ErrorMessage = "Membership must be 'Gold', 'Diamond', or 'Normal'.")]
        public Membership Membership { get; set; }

        [Required(ErrorMessage = "LoginEmail is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string LoginEmail { get; set; }

        //[Required(ErrorMessage = "Role is required.")]
        //[RegularExpression("^(Admin|Racer|Supervisor)$", ErrorMessage = "Role must be one of the following: Admin, Racer, Supervisor.")]
        //public string Role { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } // Plain password input

    }
}
