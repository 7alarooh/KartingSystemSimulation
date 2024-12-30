using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KartingSystemSimulation.Models
{
    public class User
    {
        [Key]
        public string LoginEmail { get; set; } // Primary Key
        public string LoginPassword { get; set; } // Hashed Password
        public Role Role { get; set; } // ENUM: Admin, Racer, Supervisor

        [JsonIgnore]
        public ICollection<Admin> Admins { get; set; }
        [JsonIgnore]
        public ICollection<Racer> Racers { get; set; }
        [JsonIgnore]
        public ICollection<Supervisor> Supervisors { get; set; }
    }
}
