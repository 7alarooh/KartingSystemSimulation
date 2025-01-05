using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KartingSystemSimulation.Models
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; } // Primary Key

        [Required]
        [ForeignKey(nameof(Racer))] // Map RacerId to Racer navigation
        public int RacerId { get; set; } // Foreign key to Racer entity

        [JsonIgnore]
        public Racer Racer { get; set; } // Navigation property to Racer

        [Required]
        public MembershipType MembershipType { get; set; } // Membership type

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int FreeTickets { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
