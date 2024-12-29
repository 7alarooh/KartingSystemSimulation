using System.ComponentModel.DataAnnotations;
using System.Net;
using KartingSystemSimulation.Enums;

namespace KartingSystemSimulation.Models
{
    public class Racer
    {
        [Key]
        public int RacerId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CivilId { get; set; } // Unique
        public string Email { get; set; } // Foreign Key to User (PK in User)
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; } // Changed from string to Enum
        public Address Address { get; set; }
        public bool AgreedToRules { get; set; }
        public byte[] Picture { get; set; } // BLOB
        public string Membership { get; set; } // ENUM: GOLD/DIAMOND/NORMAL
        public int? AssignedKartId { get; set; } // Foreign Key to Kart
        public int? SupervisorId { get; set; }
        public int? LiveRaceId { get; set; } // Foreign Key to LiveRace
        public int UserId { get; set; } // Foreign Key to User

        public User User { get; set; } // Navigation Property
        public Supervisor Supervisor { get; set; } // Navigation Property
        public Kart AssignedKart { get; set; } // Navigation Property
        public LiveRace LiveRace { get; set; } // Navigation Property
        public ICollection<RaceHistory> RaceHistories { get; set; } // Navigation Property
    }
}
