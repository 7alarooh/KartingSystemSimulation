using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class User
    {
        [Key]
        public string LoginEmail { get; set; } // Primary Key
        public string LoginPassword { get; set; } // Hashed Password
        public string Role { get; set; } // ENUM: Admin, Racer, Supervisor
    }
}
