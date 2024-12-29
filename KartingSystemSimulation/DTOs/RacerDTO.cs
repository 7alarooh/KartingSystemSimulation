namespace KartingSystemSimulation.DTOs
{
    public class RacerDTO
    {
        public int RacerId { get; set; } // Primary key
        public string FirstName { get; set; } // Racer's first name
        public string LastName { get; set; } // Racer's last name
        public string Phone { get; set; } // Contact number
        public string CivilId { get; set; } // Unique Civil ID
        public string Email { get; set; } // Validated email
        public DateTime DOB { get; set; } // Date of birth
        public string Gender { get; set; } // Gender
        public string State { get; set; } // State of residence
        public string Membership { get; set; } // Membership type (Gold/Diamond/Normal)
    }
}
