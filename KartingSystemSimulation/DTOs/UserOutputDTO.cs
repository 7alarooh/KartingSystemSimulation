namespace KartingSystemSimulation.DTOs
{
    public class UserOutputDTO
    {
        public string LoginEmail { get; set; }
        public string Role { get; set; }
        public string Token { get; set; } // Include JWT token
    }
}
