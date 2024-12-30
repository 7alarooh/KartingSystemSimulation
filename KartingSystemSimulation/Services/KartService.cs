using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class KartService : IKartService
    {
        private readonly IKartRepository _kartRepository; // Repository for kart data

        public KartService(IKartRepository kartRepository)
        {
            _kartRepository = kartRepository; // Initialize kart repository
        }

        // Add a new kart
        public void AddKart(KartInputDTO kartInput)
        {
            var kart = new Kart
            {
                KartType = kartInput.KartType, // Set kart type
                Availability = kartInput.Availability // Set availability status
            };

            _kartRepository.AddKart(kart); // Save kart to the database
        }

        // Get kart by ID
        public KartOutputDTO GetKartById(int id)
        {
            var kart = _kartRepository.GetKartById(id); // Fetch kart by ID
            if (kart == null)
            {
                throw new KeyNotFoundException("Kart not found.");
            }

            return new KartOutputDTO
            {
                KartId = kart.KartId, // Map kart ID
                KartType = kart.KartType, // Map kart type
                Availability = kart.Availability // Map availability status
            };
        }

        // Get all karts
        public IEnumerable<KartOutputDTO> GetAllKarts()
        {
            var karts = _kartRepository.GetAllKarts(); // Fetch all karts
            return karts.Select(kart => new KartOutputDTO
            {
                KartId = kart.KartId, // Map kart ID
                KartType = kart.KartType, // Map kart type
                Availability = kart.Availability // Map availability status
            }).ToList();
        }

        // Update kart details
        public void UpdateKart(int id, KartInputDTO kartInput)
        {
            var kart = _kartRepository.GetKartById(id); // Fetch kart by ID
            if (kart == null)
            {
                throw new KeyNotFoundException("Kart not found.");
            }

            kart.KartType = kartInput.KartType; // Update kart type
            kart.Availability = kartInput.Availability; // Update availability status

            _kartRepository.UpdateKart(kart); // Save updated kart to the database
        }

        // Delete kart
        public void DeleteKart(int id)
        {
            var kart = _kartRepository.GetKartById(id); // Fetch kart by ID
            if (kart == null)
            {
                throw new KeyNotFoundException("Kart not found.");
            }

            _kartRepository.DeleteKart(kart); // Delete kart from the database
        }
    }
}
