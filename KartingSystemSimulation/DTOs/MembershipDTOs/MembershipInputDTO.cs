using KartingSystemSimulation.Enums;
using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs.MembershipDTOs
{
    public class MembershipInputDTO
    {
        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // ID of the racer for whom the membership is being created

        [Required(ErrorMessage = "MembershipType is required.")]
        [EnumDataType(typeof(MembershipType), ErrorMessage = "Invalid Membership Type.")]
        public MembershipType MembershipType { get; set; } // Membership type (Normal, Gold, Diamond)

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; } = DateTime.Now; // Membership start date (default: current date)

        [Required(ErrorMessage = "DurationMonths is required.")]
        [Range(1, 12, ErrorMessage = "Duration must be between 1 and 12 months.")]
        public int DurationMonths { get; set; } // Duration in months

        [Range(0, int.MaxValue, ErrorMessage = "Free tickets must be a non-negative number.")]
        public int FreeTickets { get; set; } = 0; // Optional free tickets

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal DiscountPercentage { get; set; } = 0; // Optional discount percentage
    }
}
