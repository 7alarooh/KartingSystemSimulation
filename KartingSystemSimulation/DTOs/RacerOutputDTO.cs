namespace KartingSystemSimulation.DTOs
{
    public class RacerOutputDTO
    {
        public int RacerId { get; set; } // Primary key

        public string FirstName { get; set; } // Racer's first name

        public string LastName { get; set; } // Racer's last name

        public string Email { get; set; } // Validated email

        public DateTime DOB { get; set; } // Date of birth

        public string Gender { get; set; } // Gender

        public string Address { get; set; } // Address of residence

        public string Membership { get; set; } // Membership type (Gold/Diamond/Normal)
    }
}
