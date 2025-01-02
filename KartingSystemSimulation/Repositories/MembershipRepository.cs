using KartingSystemSimulation.DTOs.MembershipDTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Services;

namespace KartingSystemSimulation.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationDbContext _context; // Database context for accessing the database

        public MembershipRepository(ApplicationDbContext context)
        {
            _context = context; // Initialize the repository with the database context
        }

        // Add a new membership
        public void AddMembership(Membership membership)
        {
            _context.Memberships.Add(membership); // Add the membership entity to the database
            _context.SaveChanges(); // Save changes to the database
        }

        // Get a membership by ID
        public Membership GetMembershipById(int membershipId)
        {
            return _context.Memberships.FirstOrDefault(m => m.MembershipId == membershipId); // Retrieve the membership by ID
        }

        // Get all memberships
        public IEnumerable<Membership> GetAllMemberships()
        {
            return _context.Memberships.ToList(); // Retrieve all memberships
        }

        // Update an existing membership
        public void UpdateMembership(Membership membership)
        {
            _context.Memberships.Update(membership); // Update the membership entity
            _context.SaveChanges(); // Save changes to the database
        }

        // Delete a membership
        public void DeleteMembership(Membership membership)
        {
            _context.Memberships.Remove(membership); // Remove the membership entity from the database
            _context.SaveChanges(); // Save changes to the database
        }
    }
}
