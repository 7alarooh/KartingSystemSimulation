using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class RacerSupervisorDTO
    {
        public string Name { get; set; } // Supervisor's name

        public string CivilId { get; set; } // Unique Civil ID

        public string Phone { get; set; } // Contact number

        public string Email { get; set; } // Validated email

        public string Password { get; set; }
    }
}
