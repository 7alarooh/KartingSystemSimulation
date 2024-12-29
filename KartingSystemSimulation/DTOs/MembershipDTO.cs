using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class MembershipDTO
    {
        [Required(ErrorMessage = "MembershipId is required.")]
        public int MembershipId { get; set; } // Primary key

        [Required(ErrorMessage = "Membership Type is required.")]
        [StringLength(20, ErrorMessage = "Membership Type cannot be longer than 20 characters.")]
        [RegularExpression(@"^(Gold|Diamond|Normal)$", ErrorMessage = "Membership Type must be either Gold, Diamond, or Normal.")]
        public string MembershipType { get; set; } // Membership type (Gold, Diamond, Normal)

        [Required(ErrorMessage = "Discount is required.")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public int Discount { get; set; } // Discount percentage

        [Required(ErrorMessage = "Free Tickets is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Free Tickets must be a non-negative number.")]
        public int FreeTickets { get; set; } // Number of free tickets
    }
}
