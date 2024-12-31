using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KartingSystemSimulation.Services
{


    public class RacerService : IRacerService
    {
        private readonly IRacerRepository _racerRepository;
        private readonly IUserService _userService;

        public RacerService(IRacerRepository racerRepository, IUserService userService)
        {
            _racerRepository = racerRepository; // Initialize racer repository
            _userService = userService;
        }

        // Add a new racer
        public void AddRacer(RacerInputDTO racerInput)
        {
            // Calculate age from DOB
            var age = CalculateAge(racerInput.DOB);

            // Validate supervisor details for racers under 18
            if (age < 18 && racerInput.SupervisorId == null)
            {
                throw new InvalidOperationException("Supervisor details are required for racers under 18.");
            }
            var user = new UserInputDTO
            {
                LoginEmail = racerInput.LoginEmail,
                Password = racerInput.Password,
                Role = racerInput.Role,
            };
            var userTest = _userService.TestAddUser(user);

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
                SupervisorId = age < 18 ? racerInput.SupervisorId : null, // Assign supervisor only if age < 18
                Membership = racerInput.Membership,
                User = userTest
            };

            
            _racerRepository.AddRacer(racer); // Save the racer
        }

        // Helper method to calculate age
        private int CalculateAge(DateTime dob)
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
