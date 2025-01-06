using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Repositories
{
    public interface IMembershipRepository
    {
        void AddMembership(Membership membership);
        void DeleteMembership(Membership membership);
        IEnumerable<Membership> GetAllMemberships();
        Membership GetMembershipById(int membershipId);
        IEnumerable<Supervisor> GetRelatedSupervisors(int racerId);
        void UpdateMembership(Membership membership);
    }
}