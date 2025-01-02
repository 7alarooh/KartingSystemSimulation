﻿// <auto-generated />
using System;
using KartingSystemSimulation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KartingSystemSimulation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250101115731_Test 2")]
    partial class Test2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KartingSystemSimulation.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<int>("Address")
                        .HasColumnType("int");

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserLoginEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdminId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserLoginEmail");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<int>("KartId")
                        .HasColumnType("int");

                    b.Property<int>("Laps")
                        .HasColumnType("int");

                    b.Property<int?>("LiveRaceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RaceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RaceType")
                        .HasColumnType("int");

                    b.Property<string>("TopRacers")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.HasIndex("KartId")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Kart", b =>
                {
                    b.Property<int>("KartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KartId"));

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int>("KartType")
                        .HasColumnType("int");

                    b.HasKey("KartId");

                    b.ToTable("Karts");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Leaderboard", b =>
                {
                    b.Property<int>("LeaderboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaderboardId"));

                    b.Property<TimeSpan>("BestTiming")
                        .HasColumnType("time");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<int>("RacerId")
                        .HasColumnType("int");

                    b.HasKey("LeaderboardId");

                    b.HasIndex("RacerId");

                    b.ToTable("Leaderboards");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.LiveRace", b =>
                {
                    b.Property<int>("LiveRaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LiveRaceId"));

                    b.Property<int>("CurrentLap")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("LapTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("RaceDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TotalTime")
                        .HasColumnType("time");

                    b.Property<string>("UpdateDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LiveRaceId");

                    b.HasIndex("GameId");

                    b.ToTable("LiveRaces");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceBooking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingType")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("RacerId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("RaceId");

                    b.HasIndex("RacerId");

                    b.ToTable("RaceBookings");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceHistory", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoryId"));

                    b.Property<TimeSpan>("BestTiming")
                        .HasColumnType("time");

                    b.Property<int>("RaceHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("RacerId")
                        .HasColumnType("int");

                    b.Property<int>("StarsEarned")
                        .HasColumnType("int");

                    b.HasKey("HistoryId");

                    b.HasIndex("RacerId");

                    b.ToTable("RaceHistories");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceHistoryLeaderboard", b =>
                {
                    b.Property<int>("RaceHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("LeaderboardId")
                        .HasColumnType("int");

                    b.HasKey("RaceHistoryId", "LeaderboardId");

                    b.HasIndex("LeaderboardId");

                    b.ToTable("RaceHistoryLeaderboards");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceRacer", b =>
                {
                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("RacerId")
                        .HasColumnType("int");

                    b.HasKey("RaceId", "RacerId");

                    b.HasIndex("RacerId");

                    b.ToTable("RaceRacers");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Racer", b =>
                {
                    b.Property<int>("RacerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RacerId"));

                    b.Property<int>("Address")
                        .HasColumnType("int");

                    b.Property<bool>("AgreedToRules")
                        .HasColumnType("bit");

                    b.Property<int?>("AssignedKartId")
                        .HasColumnType("int");

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LiveRaceId")
                        .HasColumnType("int");

                    b.Property<int>("Membership")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserLoginEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RacerId");

                    b.HasIndex("AssignedKartId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LiveRaceId");

                    b.HasIndex("SupervisorId");

                    b.HasIndex("UserLoginEmail");

                    b.ToTable("Racers");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Supervisor", b =>
                {
                    b.Property<int>("SupervisorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupervisorId"));

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLoginEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SupervisorId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserLoginEmail");

                    b.ToTable("Supervisors");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.SupervisorRacer", b =>
                {
                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.Property<int>("RacerId")
                        .HasColumnType("int");

                    b.HasKey("SupervisorId", "RacerId");

                    b.HasIndex("RacerId");

                    b.ToTable("SupervisorRacers");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.User", b =>
                {
                    b.Property<string>("LoginEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("LoginEmail");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Admin", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("KartingSystemSimulation.Models.Admin", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.User", null)
                        .WithMany("Admins")
                        .HasForeignKey("UserLoginEmail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Game", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Kart", "Kart")
                        .WithOne()
                        .HasForeignKey("KartingSystemSimulation.Models.Game", "KartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kart");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Leaderboard", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Racer", "Racer")
                        .WithMany()
                        .HasForeignKey("RacerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Racer");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.LiveRace", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Game", "Game")
                        .WithMany("LiveRaceUpdates")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceBooking", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.Racer", "Racer")
                        .WithMany()
                        .HasForeignKey("RacerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Racer");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceHistory", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Racer", "Racer")
                        .WithMany("RaceHistories")
                        .HasForeignKey("RacerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Racer");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceHistoryLeaderboard", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Leaderboard", "Leaderboard")
                        .WithMany("RaceHistoryEvaluations")
                        .HasForeignKey("LeaderboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.RaceHistory", "RaceHistory")
                        .WithMany("LeaderboardEvaluations")
                        .HasForeignKey("RaceHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Leaderboard");

                    b.Navigation("RaceHistory");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceRacer", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.Racer", "Racer")
                        .WithMany()
                        .HasForeignKey("RacerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Racer");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Racer", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Kart", "AssignedKart")
                        .WithMany()
                        .HasForeignKey("AssignedKartId");

                    b.HasOne("KartingSystemSimulation.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("KartingSystemSimulation.Models.Racer", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.LiveRace", "LiveRace")
                        .WithMany("Racers")
                        .HasForeignKey("LiveRaceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("KartingSystemSimulation.Models.Supervisor", "Supervisor")
                        .WithMany("SupervisedRacers")
                        .HasForeignKey("SupervisorId");

                    b.HasOne("KartingSystemSimulation.Models.User", null)
                        .WithMany("Racers")
                        .HasForeignKey("UserLoginEmail");

                    b.Navigation("AssignedKart");

                    b.Navigation("LiveRace");

                    b.Navigation("Supervisor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Supervisor", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("KartingSystemSimulation.Models.Supervisor", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.User", null)
                        .WithMany("Supervisors")
                        .HasForeignKey("UserLoginEmail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.SupervisorRacer", b =>
                {
                    b.HasOne("KartingSystemSimulation.Models.Racer", "Racer")
                        .WithMany()
                        .HasForeignKey("RacerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KartingSystemSimulation.Models.Supervisor", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Racer");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Game", b =>
                {
                    b.Navigation("LiveRaceUpdates");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Leaderboard", b =>
                {
                    b.Navigation("RaceHistoryEvaluations");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.LiveRace", b =>
                {
                    b.Navigation("Racers");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.RaceHistory", b =>
                {
                    b.Navigation("LeaderboardEvaluations");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Racer", b =>
                {
                    b.Navigation("RaceHistories");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.Supervisor", b =>
                {
                    b.Navigation("SupervisedRacers");
                });

            modelBuilder.Entity("KartingSystemSimulation.Models.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Racers");

                    b.Navigation("Supervisors");
                });
#pragma warning restore 612, 618
        }
    }
}
