using KartingSystemSimulation.Models;
using Microsoft.EntityFrameworkCore;

namespace KartingSystemSimulation
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Racer> Racers { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<SupervisorRacer> SupervisorRacers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Kart> Karts { get; set; }
        public DbSet<RaceBooking> RaceBookings { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<RaceHistory> RaceHistories { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<LiveRace> LiveRaces { get; set; }
        public DbSet<RaceRacer> RaceRacers { get; set; }
        public DbSet<RaceHistoryLeaderboard> RaceHistoryLeaderboards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Table Configuration
            modelBuilder.Entity<User>()
                .HasKey(u => u.LoginEmail); // Set LoginEmail as the Primary Key

            // Admin-User Relationship
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a => a.Email)
                .OnDelete(DeleteBehavior.Cascade);

            // Racer-User Relationship
            modelBuilder.Entity<Racer>()
                .HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<Racer>(r => r.Email)
                .OnDelete(DeleteBehavior.Cascade);

            // Supervisor-User Relationship
            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Supervisor>(s => s.Email)
                .OnDelete(DeleteBehavior.Cascade);

            // Supervisor_Racer Many-to-Many Relation
            modelBuilder.Entity<SupervisorRacer>()
                .HasKey(sr => new { sr.SupervisorId, sr.RacerId }); // Composite Key

            modelBuilder.Entity<SupervisorRacer>()
                .HasOne(sr => sr.Supervisor)
                .WithMany()
                .HasForeignKey(sr => sr.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for Supervisor

            modelBuilder.Entity<SupervisorRacer>()
                .HasOne(sr => sr.Racer)
                .WithMany()
                .HasForeignKey(sr => sr.RacerId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete for Racer

            // Game and Kart Relation
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Kart)
                .WithOne()
                .HasForeignKey<Game>(g => g.KartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Game and LiveRace Relation
            modelBuilder.Entity<LiveRace>()
                .HasOne(lr => lr.Game)
                .WithMany(g => g.LiveRaceUpdates)
                .HasForeignKey(lr => lr.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // LiveRace to Racer Relation (One-to-Many)
            modelBuilder.Entity<LiveRace>()
                .HasMany(lr => lr.Racers)
                .WithOne(r => r.LiveRace)
                .HasForeignKey(r => r.LiveRaceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Racer and RaceHistory Relation
            modelBuilder.Entity<RaceHistory>()
                .HasOne(rh => rh.Racer)
                .WithMany(r => r.RaceHistories)
                .HasForeignKey(rh => rh.RacerId)
                .OnDelete(DeleteBehavior.Cascade);

            // RaceHistory and Leaderboard Many-to-Many Relation
            modelBuilder.Entity<RaceHistoryLeaderboard>()
                .HasKey(rhl => new { rhl.RaceHistoryId, rhl.LeaderboardId });

            modelBuilder.Entity<RaceHistoryLeaderboard>()
                .HasOne(rhl => rhl.RaceHistory)
                .WithMany(rh => rh.LeaderboardEvaluations)
                .HasForeignKey(rhl => rhl.RaceHistoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RaceHistoryLeaderboard>()
                .HasOne(rhl => rhl.Leaderboard)
                .WithMany(l => l.RaceHistoryEvaluations)
                .HasForeignKey(rhl => rhl.LeaderboardId)
                .OnDelete(DeleteBehavior.Cascade);

            // RaceBooking Relations
            modelBuilder.Entity<RaceBooking>()
                .HasOne(rb => rb.Racer)
                .WithMany()
                .HasForeignKey(rb => rb.RacerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RaceBooking>()
                .HasOne(rb => rb.Game)
                .WithMany()
                .HasForeignKey(rb => rb.RaceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Race_Racer Many-to-Many Relation
            modelBuilder.Entity<RaceRacer>()
                .HasKey(rr => new { rr.RaceId, rr.RacerId });

            modelBuilder.Entity<RaceRacer>()
                .HasOne(rr => rr.Game)
                .WithMany()
                .HasForeignKey(rr => rr.RaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RaceRacer>()
                .HasOne(rr => rr.Racer)
                .WithMany()
                .HasForeignKey(rr => rr.RacerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
