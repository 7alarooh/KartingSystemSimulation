using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.DTOs.MembershipDTOs;
using KartingSystemSimulation.Enums;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;
using System;

namespace KartingSystemSimulation.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRacerRepository _racerRepository;

        public MembershipService(IMembershipRepository membershipRepository, IRacerRepository racerRepository)
        {
            _membershipRepository = membershipRepository; // Inject Membership Repository
            _racerRepository = racerRepository; // Inject Racer Repository
        }

        // Add a new membership
        public MembershipOutputDTO AddMembership(MembershipInputDTO membershipInput)
        {
            // Validate if Racer exists
            var racer = _racerRepository.GetRacerById(membershipInput.RacerId);
            if (racer == null)
            {
                throw new InvalidOperationException("Racer not found.");
            }

            // Calculate end date
            var endDate = membershipInput.StartDate.AddMonths(membershipInput.DurationMonths);

            // Create a Membership entity
            var membership = new Membership
            {
                RacerId = membershipInput.RacerId,
                MembershipType = membershipInput.MembershipType,
                StartDate = membershipInput.StartDate,
                EndDate = endDate,
                FreeTickets = membershipInput.FreeTickets,
                DiscountPercentage = membershipInput.DiscountPercentage
            };

            // Save membership in the database
            _membershipRepository.AddMembership(membership);

            // Prepare and return output DTO
            return new MembershipOutputDTO
            {
                MembershipId = membership.MembershipId,
                RacerId = membership.RacerId,
                MembershipType = membership.MembershipType,
                StartDate = membership.StartDate,
                EndDate = membership.EndDate,
                FreeTicketsRemaining = membership.FreeTickets,
                DiscountPercentage = membership.DiscountPercentage,
                RacerName = $"{racer.FirstName} {racer.LastName}"
            };
        }

        // Get a membership by ID
        public MembershipOutputDTO GetMembershipById(int membershipId)
        {
            var membership = _membershipRepository.GetMembershipById(membershipId);
            if (membership == null)
            {
                throw new InvalidOperationException("Membership not found.");
            }

            var racer = _racerRepository.GetRacerById(membership.RacerId);

            return new MembershipOutputDTO
            {
                MembershipId = membership.MembershipId,
                RacerId = membership.RacerId,
                MembershipType = membership.MembershipType,
                StartDate = membership.StartDate,
                EndDate = membership.EndDate,
                FreeTicketsRemaining = membership.FreeTickets,
                DiscountPercentage = membership.DiscountPercentage,
                RacerName = $"{racer.FirstName} {racer.LastName}"
            };
        }

        // Get all memberships
        public IEnumerable<MembershipOutputDTO> GetAllMemberships()
        {
            var memberships = _membershipRepository.GetAllMemberships();
            return memberships.Select(m =>
            {
                var racer = _racerRepository.GetRacerById(m.RacerId);
                return new MembershipOutputDTO
                {
                    MembershipId = m.MembershipId,
                    RacerId = m.RacerId,
                    MembershipType = m.MembershipType,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    FreeTicketsRemaining = m.FreeTickets,
                    DiscountPercentage = m.DiscountPercentage,
                    RacerName = $"{racer.FirstName} {racer.LastName}"
                };
            }).ToList();
        }
        // Update an existing membership
        public bool UpdateMembership(int membershipId, MembershipInputDTO membershipInput)
        {
            var membership = _membershipRepository.GetMembershipById(membershipId);
            if (membership == null) return false;

            membership.MembershipType = membershipInput.MembershipType;
            membership.StartDate = membershipInput.StartDate;
            membership.EndDate = membershipInput.StartDate.AddMonths(membershipInput.DurationMonths);
            membership.FreeTickets = membershipInput.MembershipType == MembershipType.Dimond ? 10 : 0;
            membership.DiscountPercentage = membershipInput.MembershipType == MembershipType.Gold ? 50 :
                                             membershipInput.MembershipType == MembershipType.Dimond ? 40 : 0;

            _membershipRepository.UpdateMembership(membership);
            return true;
        }

        // Delete a membership
        public bool DeleteMembership(int membershipId)
        {
            var membership = _membershipRepository.GetMembershipById(membershipId);
            if (membership == null) return false;

            _membershipRepository.DeleteMembership(membership);
            return true;
        }
    }
}
