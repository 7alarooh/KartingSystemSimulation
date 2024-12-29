namespace KartingSystemSimulation.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; } // Primary key
        public string LoginEmail { get; set; } // User's login email
        public string Role { get; set; } // User's role (Admin, Racer, Supervisor)
    }
}
