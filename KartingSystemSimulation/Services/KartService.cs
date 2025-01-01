using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    /// <summary>
    /// Provides business logic for managing Karts.
    /// </summary>
    public class KartService : IKartService
    {
        private readonly IKartRepository _kartRepository; // Dependency injection for the Kart repository.

        /// <summary>
        /// Constructor to initialize the Kart service with a repository.
        /// </summary>
        /// <param name="kartRepository">Repository for interacting with the database.</param>
        public KartService(IKartRepository kartRepository)
        {
            _kartRepository = kartRepository;
        }

        /// <summary>
        /// Adds a new kart to the system.
        /// </summary>
        /// <param name="kartInput">DTO containing kart details.</param>
        public void AddKart(KartInputDTO kartInput)
        {
            // Validate that kartInput is not null
            if (kartInput == null)
            {
                throw new ArgumentNullException(nameof(kartInput), "Kart input cannot be null.");
            }

            // Check if the KartType is valid (no parsing required since it's already an enum)
            if (!Enum.IsDefined(typeof(KartType), kartInput.KartType))
            {
                throw new ArgumentException("Invalid kart type specified.", nameof(kartInput.KartType));
            }

            // Check for unique KartId to prevent duplicates
            var existingKart = _kartRepository.GetKartById(kartInput.KartId);
            if (existingKart != null)
            {
                throw new InvalidOperationException($"Kart with ID {kartInput.KartId} already exists.");
            }

            // Map input DTO to a new Kart object
            var kart = new Kart
            {
                KartId = kartInput.KartId,
                KartType = kartInput.KartType, // Directly assign the KartType enum
                Availability = kartInput.Availability
            };

            try
            {
                // Add the kart to the repository and persist in the database
                _kartRepository.AddKart(kart);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the kart.", ex);
            }
        }






        /// <summary>
        /// Retrieves details of a kart by its ID.
        /// </summary>
        /// <param name="id">Kart ID.</param>
        /// <returns>Kart details as an output DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the kart is not found.</exception>
        public KartOutputDTO GetKartById(int id)
        {
            var kart = _kartRepository.GetKartById(id);
            if (kart == null)
            {
                throw new KeyNotFoundException("Kart not found.");
            }

            return new KartOutputDTO
            {
                KartId = kart.KartId,
                KartType = kart.KartType, // Assign enum directly.
                Availability = kart.Availability
            };
        }

        /// <summary>
        /// Retrieves all karts in the system.
        /// </summary>
        /// <returns>A list of kart details as output DTOs.</returns>
        public IEnumerable<KartOutputDTO> GetAllKarts()
        {
            var karts = _kartRepository.GetAllKarts();

            return karts.Select(kart => new KartOutputDTO
            {
                KartId = kart.KartId,
                KartType = kart.KartType, // Assign enum directly.
                Availability = kart.Availability
            }).ToList();
        }


        /// <summary>
        /// Updates the details of an existing kart.
        /// </summary>
        /// <param name="id">Kart ID to be updated.</param>
        /// <param name="kartInput">DTO containing updated kart details.</param>
        /// <exception cref="KeyNotFoundException">Thrown if the kart is not found.</exception>
        public void UpdateKart(int id, KartInputDTO kartInput)
        {
            var kart = _kartRepository.GetKartById(id);
            if (kart == null)
            {
                throw new KeyNotFoundException("Kart not found.");
            }

            // Again, no need for Enum.Parse — the input is already an enum.
            kart.KartId = kartInput.KartId;
            kart.KartType = kartInput.KartType;
            kart.Availability = kartInput.Availability;

            _kartRepository.UpdateKart(kart);
        }

        /// <summary>
        /// Deletes a kart by its ID.
        /// </summary>
        /// <param name="id">Kart ID to be deleted.</param>
        /// <exception cref="KeyNotFoundException">Thrown if the kart is not found.</exception>
        public void DeleteKart(int id)
        {
            // Fetch the kart to be deleted from the repository.
            var kart = _kartRepository.GetKartById(id);
            if (kart == null)
            {
                throw new KeyNotFoundException("Kart not found.");
            }

            // Remove the kart from the repository.
            _kartRepository.DeleteKart(kart);
        }
    }
}
