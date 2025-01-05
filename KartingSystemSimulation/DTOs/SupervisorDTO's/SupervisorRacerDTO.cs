using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.DTOs
{
    public class SupervisorRacerDTO
    {
        [Required(ErrorMessage = "SupervisorId is required.")]
        public int SupervisorId { get; set; } // Foreign key to Supervisor

        [Required(ErrorMessage = "RacerId is required.")]
        public int RacerId { get; set; } // Foreign key to Racer
    }
}
