using KartingSystemSimulation.DTOs.MembershipDTOs;

namespace KartingSystemSimulation.Services
{
    public interface IMembershipService
    {
        MembershipOutputDTO AddMembership(MembershipInputDTO membershipInput);
        bool DeleteMembership(int membershipId);
        IEnumerable<MembershipOutputDTO> GetAllMemberships();
        MembershipOutputDTO GetMembershipById(int membershipId);
        bool IsSupervisorRelatedToRacer(string supervisorEmail, int membershipId);
        bool UpdateMembership(int membershipId, MembershipInputDTO membershipInput);
        bool IsRacerMembershipOwner(string racerEmail, int membershipId);
    }
}