using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; } // Primary Key

        [Required]
        public int RacerId { get; set; } // Foreign key to Racer entity

        [Required]
        public string MembershipType { get; set; } // Membership type (e.g., Normal, Gold, Diamond)

        [Required]
        public DateTime StartDate { get; set; } // Start date of the membership

        [Required]
        public DateTime EndDate { get; set; } // End date of the membership

        public int FreeTickets { get; set; } // Free tickets provided with the membership

        public decimal DiscountPercentage { get; set; } // Discount percentage offered with the membership
    }
}
