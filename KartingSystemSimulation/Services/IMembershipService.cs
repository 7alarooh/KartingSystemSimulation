using KartingSystemSimulation.DTOs.MembershipDTOs;

namespace KartingSystemSimulation.Services
{
    public interface IMembershipService
    {
        MembershipOutputDTO AddMembership(MembershipInputDTO membershipInput);
        bool DeleteMembership(int membershipId);
        IEnumerable<MembershipOutputDTO> GetAllMemberships();
        MembershipOutputDTO GetMembershipById(int membershipId);
        bool UpdateMembership(int membershipId, MembershipInputDTO membershipInput);
    }
}