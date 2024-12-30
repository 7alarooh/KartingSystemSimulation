using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using KartingSystemSimulation.Enums;

namespace KartingSystemSimulation.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; } // Primary Key
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CivilId { get; set; } // Unique
        [EmailAddress]
        
        [ForeignKey("User")]
        public string Email { get; set; } //Foreign Key to User (PK in User)
        [Required]
        public Gender Gender { get; set; } // Changed from string to Enum
        [Required]
        public Address Address { get; set; }
        public byte[]? Picture { get; set; } // BLOB

        public User User { get; set; } // Navigation Property
    }
}
