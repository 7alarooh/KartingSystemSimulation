namespace KartingSystemSimulation.DTOs
{
    public class SupervisorRacerDTO
    {
        public int SupervisorId { get; set; } // Foreign key to Supervisor
        public int RacerId { get; set; } // Foreign key to Racer
    }
}
