using System.ComponentModel.DataAnnotations;

namespace KartingSystemSimulation.Models
{
    public class Supervisor
    {
        [Key]
        public int SupervisorId { get; set; } // Primary Key
        public string Name { get; set; }
        public string CivilId { get; set; } // Unique
        public string Phone { get; set; }
        public string Email { get; set; } // Foreign Key to User (PK in User)
        
        public User User { get; set; }
        public ICollection<Racer> SupervisedRacers { get; set; }
    }

}
