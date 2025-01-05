using KartingSystemSimulation.DTOs.MembershipDTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RacerOutputDTO
    {
        [Required]
        public int RacerId { get; set; } // Unique identifier for the racer

        [Required]
        public string FirstName { get; set; } // Racer's first name

        [Required]
        public string LastName { get; set; } // Racer's last name

        [Required]
        public string Email { get; set; } // Racer's email address

        [Required]
        public DateTime DOB { get; set; } // Racer's date of birth
        [Required]
        public Gender Gender { get; set; } // Racer's gender

        public Address Address { get; set; } // Racer's state of residence

        public bool AgreedToRules { get; set; } // Indicates if the racer agreed to rules
                                                // Include membership details as MembershipOutputDTO
        public MembershipOutputDTO Membership { get; set; }  // Racer's Membership type
    }
}
