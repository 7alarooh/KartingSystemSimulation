using KartingSystemSimulation.DTOs.MembershipDTOs;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService; // Membership service for business logic

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        // Get all memberships
        [HttpGet]
        public ActionResult<IEnumerable<MembershipOutputDTO>> GetAllMemberships()
        {
            string token = JwtHelper.ExtractToken(Request);
            var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
            var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

            // Validate role
            if (role != "Admin")
                return Forbid("Only Admins can access this resource.");

            var memberships = _membershipService.GetAllMemberships();
            return Ok(memberships);
        }

        // Get membership by ID
        [HttpGet("{membershipId}")]
        public ActionResult<MembershipOutputDTO> GetMembershipById(int membershipId)
        {
            string token = JwtHelper.ExtractToken(Request);
            var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
            var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

            // Validate role
            if (role != "Admin")
                return Forbid("Only Admins can access this resource.");

            var membership = _membershipService.GetMembershipById(membershipId);
            if (membership == null)
                return NotFound($"Membership with ID {membershipId} not found.");

            return Ok(membership);
        }

        // Add a new membership
        [HttpPost]
        public ActionResult AddMembership([FromBody] MembershipInputDTO membershipInput)
        {
            string token = JwtHelper.ExtractToken(Request);
            var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
            var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

            // Validate role
            if (role != "Admin")
                return Forbid("Only Admins can access this resource.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _membershipService.AddMembership(membershipInput);
            return CreatedAtAction(nameof(GetMembershipById), new { membershipId = membershipInput.RacerId }, membershipInput);
        }

        // Update an existing membership
        [HttpPut("{membershipId}")]
        public ActionResult UpdateMembership(int membershipId, [FromBody] MembershipInputDTO membershipInput)
        {
            string token = JwtHelper.ExtractToken(Request);
            var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
            var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

            // Validate role
            if (role != "Admin")
                return Forbid("Only Admins can access this resource.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _membershipService.UpdateMembership(membershipId, membershipInput);
            if (!success)
                return NotFound($"Membership with ID {membershipId} not found.");

            return NoContent();
        }

        // Delete a membership
        [HttpDelete("{membershipId}")]
        public ActionResult DeleteMembership(int membershipId)
        {
            string token = JwtHelper.ExtractToken(Request);
            var email = JwtHelper.GetClaimValue(token, JwtRegisteredClaimNames.Email);
            var role = JwtHelper.GetClaimValue(token, ClaimTypes.Role);

            // Validate role
            if (role != "Admin")
                return Forbid("Only Admins can access this resource.");
            var success = _membershipService.DeleteMembership(membershipId);
            if (!success)
                return NotFound($"Membership with ID {membershipId} not found.");

            return NoContent();
        }
    }
}
