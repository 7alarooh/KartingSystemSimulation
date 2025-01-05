namespace KartingSystemSimulation.DTOs
{
    public class AdminOutputDTO
    {
        public int AdminId { get; set; } // Primary key

        public string FirstName { get; set; } // Admin's first name

        public string LastName { get; set; } // Admin's last name

        public string Phone { get; set; } // Contact number

        public string CivilId { get; set; } // Unique Civil ID

        public string Email { get; set; } // Validated email

        public string Gender { get; set; } // Gender

        public string Address { get; set; } // Address of residence
    }

}
