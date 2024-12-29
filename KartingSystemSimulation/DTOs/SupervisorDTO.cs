namespace KartingSystemSimulation.DTOs
{
    public class SupervisorDTO
    {
        public int SupervisorId { get; set; } // Primary key
        public string Name { get; set; } // Supervisor's name
        public string CivilId { get; set; } // Unique Civil ID
        public string Phone { get; set; } // Contact number
        public string Email { get; set; } // Validated email
    }
}
