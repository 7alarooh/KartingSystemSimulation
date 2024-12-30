﻿using KartingSystemSimulation.DTOs;
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

        public RacerService(IRacerRepository racerRepository)
        {
            _racerRepository = racerRepository; // Initialize racer repository
        }

        // Add a new racer
        public void AddRacer(RacerInputDTO racerInput)
        {
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
                SupervisorId = racerInput.SupervisorId // Assign supervisor if applicable
            };

            _racerRepository.AddRacer(racer);
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
