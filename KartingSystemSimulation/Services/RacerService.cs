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
        private readonly IUserRepository _userRepository; // To get the current user information (to check for admin and self-deletion)
        private readonly RacerRepository _repository;
        public RacerService(RacerRepository repository)
        {
            _repository = repository;
        }
        public RacerService(IRacerRepository racerRepository, IUserRepository userRepository)
        {
            _racerRepository = racerRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Racer> GetAllRacers()
        {
            return _racerRepository.GetAllRacers();
        }

        public Racer GetRacerById(int racerId)
        {
            return _racerRepository.GetRacerById(racerId);
        }

        public void AddRacer(RacerInputDTO racerDto, int currentUserId)
        {
            // Check if first name and last name are the same
            if (racerDto.FirstName.Equals(racerDto.LastName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("First name and last name cannot be the same.");
            }

            // Check if age is at least 6 years old
            var today = DateTime.Today;
            var age = today.Year - racerDto.DOB.Year;
            if (racerDto.DOB.Date > today.AddYears(-age)) age--; // Adjust for birth date not yet reached in current year

            if (age < 6)
            {
                throw new InvalidOperationException("Racer must be at least 6 years old.");
            }

            // Map DTO to Racer entity
            var racer = new Racer
            {
                FirstName = racerDto.FirstName,
                LastName = racerDto.LastName,
                Phone = racerDto.Phone,
                CivilId = racerDto.CivilId,
                Email = racerDto.Email,
                DOB = racerDto.DOB,
                //Membership = racerDto.Membership,
                SupervisorId = currentUserId
            };

            // Save the racer to the database
            _racerRepository.AddRacer(racer);
        }


        public void UpdateRacer(Racer racer)
        {
            // Check if first name and last name are the same
            if (racer.FirstName.Equals(racer.LastName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("First name and last name cannot be the same.");
            }

            // Check if age is at least 6 years old
            var today = DateTime.Today;
            var age = today.Year - racer.DOB.Year;
            if (racer.DOB.Date > today.AddYears(-age)) age--; // Adjust for birth date not yet reached in current year

            if (age < 6)
            {
                throw new InvalidOperationException("Racer must be at least 6 years old.");
            }

            // Ensure racer exists in the database before updating
            var existingRacer = _racerRepository.GetRacerById(racer.RacerId);
            if (existingRacer == null)
            {
                throw new InvalidOperationException("Racer not found.");
            }

            // Perform the update in the repository
            _racerRepository.UpdateRacer(racer);
        }


        public void DeleteRacer(int racerId, int currentUserId)
        {
            // Retrieve the current user (admin) details
            var currentUser = _userRepository.GetUserById(currentUserId);  // Assuming we pass the currentUserId
            var racer = _racerRepository.GetRacerById(racerId);

            if (racer == null)
            {
                throw new InvalidOperationException("Racer not found.");
            }

            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user not found.");
            }


        }
        // Delete Racer - Admin-Only Operation
        public void DeleteRacer(int racerId, string role)
        {
            if (role != "Admin") // Check if the user is an admin
            {
                throw new UnauthorizedAccessException("Only admins can delete racers.");
            }
            var racer = _repository.GetRacerById(racerId);
            if (racer != null)
            {
                _repository.DeleteRacer(racer); // Perform the deletion
            }
        }
    }
}
