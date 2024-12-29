using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class Admin
    {
        [Key] 
        public int AdminId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CivilId { get; set; } // Unique
        public string Email { get; set; } //Foreign Key to User (PK in User)
        public string Gender { get; set; }
        public string State { get; set; }
        public byte[] Picture { get; set; } // BLOB

        public User User { get; set; } // Navigation Property
    }
}
