using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("User")]
        public string Email { get; set; }

        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public bool AgreedToRules { get; set; }
        public byte[]? Picture { get; set; }

        // Configure one-to-one navigation
        //
        public Membership Membership { get; set; }

        public int? AssignedKartId { get; set; }
        public int? SupervisorId { get; set; }
        public int? LiveRaceId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Supervisor Supervisor { get; set; }
        public Kart AssignedKart { get; set; }
        public LiveRace LiveRace { get; set; }
        public ICollection<RaceHistory> RaceHistories { get; set; }
    }
}
