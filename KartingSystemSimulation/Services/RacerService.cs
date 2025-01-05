using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KartingSystemSimulation.Services
{


    public class RacerService : IRacerService
    {
        private readonly IRacerRepository _racerRepository;
        private readonly IUserService _userService;
        private readonly ISupervisorService _SupervisorService;
        private readonly ApplicationDbContext _context;
        private readonly IMembershipService _membershipService;

        private readonly IEmailService _emailService;

        public RacerService(IRacerRepository racerRepository, IUserService userService, ISupervisorService supervisorService, IEmailService EmailService, IMembershipService membershipService)
        {
            _racerRepository = racerRepository; // Initialize racer repository
            _userService = userService;
            _SupervisorService = supervisorService;
            _emailService = EmailService;
            _membershipService = membershipService;
        }

        // Add a new racer
        public void AddRacer(RacerInputDTO racerInput)
        {
            using var transaction = _context.Database.BeginTransaction();            // Begin transaction
            try
            {
                // Step 1: Calculate age
                var age = CalculateAge(racerInput.DOB);

                // Step 2: Add supervisor if under 18
                Supervisor supervisor = null;
                if (age < 18)
                {
                    var supervisorDto = new SupervisorInputDTO
                    {
                        Name = racerInput.supervisor.Name,
                        Email = racerInput.supervisor.Email,
                        CivilId = racerInput.supervisor.CivilId,
                        Phone = racerInput.supervisor.Phone
                    };
                    supervisor = _SupervisorService.AddSupervisor(supervisorDto); // Save supervisor
                }

                // Step 3: Add user
                var user = new UserInputDTO
                {
                    LoginEmail = racerInput.LoginEmail,
                    Password = racerInput.Password
                };
                var userEntity = _userService.TestAddUser(user);

                // Step 4: Add racer
                var racer = new Racer
                {
                    FirstName = racerInput.FirstName,
                    LastName = racerInput.LastName,
                    Phone = racerInput.Phone,
                    CivilId = racerInput.CivilId,
                    Email = racerInput.Email,
                    DOB = racerInput.DOB,
                    Gender = racerInput.Gender,
                    Address = racerInput.Address,
                    AgreedToRules = racerInput.AgreedToRules,
                    SupervisorId = supervisor?.SupervisorId, // Assign supervisor if applicable
                    Membership = racerInput.Membership,
                    User = userEntity
                };

                _racerRepository.AddRacer(racer);
                _context.SaveChanges(); // Commit changes to database

                transaction.Commit(); // Commit transaction

                // Step 5: Send email notification
                // Email notification
                string subject = "Welcome to Karting System!";
                string body = $@"
            <h3>Dear {racer.FirstName} {racer.LastName},</h3>
            <p>Thank you for registering with Karting System! Here are your login details:</p>
            <ul>
                <li>Email: {racer.Email}</li>
                <li>Password: {racerInput.Password}</li>
                <li>MembershipType: {racerInput.Membership.MembershipType}</li>
                <li>Remaining Free Tickets: {racerInput.Membership.FreeTickets}
            </ul>
            <p><strong>Note:</strong> Please do not share your login credentials with anyone.</p>
            <p>Enjoy your karting experience!</p>";
                _emailService.SendEmailAsync(racer.Email, subject, body).Wait();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new InvalidOperationException("Error adding racer.", ex);
            }
        }



        // Helper method to calculate age
        public int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--; // Adjust if birthday hasn't occurred yet this year
            return age;
        }

        // Get racer by ID
        public RacerOutputDTO GetRacerById(int id)
        {
            var racer = _racerRepository.GetRacerById(id);
            if (racer == null)
            {
                throw new KeyNotFoundException("Racer not found.");
            }

            return new RacerOutputDTO
            {
                RacerId = racer.RacerId,
                FirstName = racer.FirstName,
                LastName = racer.LastName,
                Email = racer.Email,
                DOB = racer.DOB,
                Gender = racer.Gender,
                Address = racer.Address,
                AgreedToRules = racer.AgreedToRules
            };
        }

        // Get all racers
        public IEnumerable<RacerOutputDTO> GetAllRacers()
        {
            var racers = _racerRepository.GetAllRacers();
            return racers.Select(racer => new RacerOutputDTO
            {
                RacerId = racer.RacerId,
                FirstName = racer.FirstName,
                LastName = racer.LastName,
                Email = racer.Email,
                DOB = racer.DOB,
                Gender = racer.Gender,
                Address = racer.Address,
                AgreedToRules = racer.AgreedToRules
            }).ToList();
        }

        // Update racer
        public void UpdateRacer(int id, RacerInputDTO racerInput)
        {
            var racer = _racerRepository.GetRacerById(id);
            if (racer == null)
            {
                throw new KeyNotFoundException("Racer not found.");
            }

            racer.FirstName = racerInput.FirstName;
            racer.LastName = racerInput.LastName;
            racer.Phone = racerInput.Phone;
            racer.CivilId = racerInput.CivilId;
            racer.Email = racerInput.Email;
            racer.DOB = racerInput.DOB;
            racer.Gender = racerInput.Gender;
            racer.Address = racerInput.Address;
            racer.AgreedToRules = racerInput.AgreedToRules;
            racer.SupervisorId = racerInput.SupervisorId;

            _racerRepository.UpdateRacer(racer);
        }

        // Delete racer
        public void DeleteRacer(int id)
        {
            var racer = _racerRepository.GetRacerById(id);
            if (racer == null)
            {
                throw new KeyNotFoundException("Racer not found.");
            }

            _racerRepository.DeleteRacer(racer);
        }
    }
}
