namespace KartingSystemSimulation.DTOs
{
    public class MembershipDTO
    {
        public int MembershipId { get; set; } // Primary key
        public string MembershipType { get; set; } // Membership type (Gold, Diamond, Normal)
        public int Discount { get; set; } // Discount percentage
        public int FreeTickets { get; set; } // Number of free tickets
    }
}
