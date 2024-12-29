using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartingSystemSimulation.Models
{
    [PrimaryKey (nameof(SupervisorId),nameof(RacerId))]
    public class SupervisorRacer
    {

        public int SupervisorId { get; set; } // Foreign Key to Supervisor
        public int RacerId { get; set; } // Foreign Key to Racer

        [ForeignKey("SupervisorId")]
        public Supervisor Supervisor { get; set; } // Navigation Property

        [ForeignKey("RacerId")]
        public Racer Racer { get; set; } // Navigation Property
    }
}
