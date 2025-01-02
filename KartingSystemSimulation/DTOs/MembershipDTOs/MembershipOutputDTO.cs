using KartingSystemSimulation.Enums;

namespace KartingSystemSimulation.DTOs.MembershipDTOs
{
    public class MembershipOutputDTO
    {
        public int MembershipId { get; set; } // Unique ID of the membership

        public int RacerId { get; set; } // ID of the associated racer

        public string MembershipType { get; set; } // Membership type (Normal, Gold, Diamond)

        public DateTime StartDate { get; set; } // Start date of the membership

        public DateTime EndDate { get; set; } // End date of the membership (calculated)

        public int FreeTicketsRemaining { get; set; } // Remaining free tickets

        public decimal DiscountPercentage { get; set; } // Discount percentage

        public string RacerName { get; set; } // Full name of the racer for display
    }
}
