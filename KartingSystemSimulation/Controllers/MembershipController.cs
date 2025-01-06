using KartingSystemSimulation.DTOs.MembershipDTOs;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        // Get all memberships (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<MembershipOutputDTO>> GetAllMemberships()
        {
            var memberships = _membershipService.GetAllMemberships();
            return Ok(memberships);
        }

        // Get membership by ID
        [HttpGet("{membershipId}")]
        public ActionResult<MembershipOutputDTO> GetMembershipById(int membershipId)
        {
            try
            {
                var currentUserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                var currentUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (currentUserRole == "Admin")
                {
                    // Admin can access any membership
                    var membership = _membershipService.GetMembershipById(membershipId);
                    if (membership == null) return NotFound($"Membership with ID {membershipId} not found.");
                    return Ok(membership);
                }

                if (currentUserRole == "Racer")
                {
                    // Racers can only access their own membership
                    if (!_membershipService.IsRacerMembershipOwner(currentUserEmail, membershipId))
                        return Forbid("Access denied: You can only access your own membership.");

                    var membership = _membershipService.GetMembershipById(membershipId);
                    if (membership == null) return NotFound($"Membership with ID {membershipId} not found.");
                    return Ok(membership);
                }

                if (currentUserRole == "Supervisor")
                {
                    // Supervisors can access memberships of related racers
                    if (!_membershipService.IsSupervisorRelatedToRacer(currentUserEmail, membershipId))
                        return Forbid("Access denied: You can only access memberships for related racers.");

                    var membership = _membershipService.GetMembershipById(membershipId);
                    if (membership == null) return NotFound($"Membership with ID {membershipId} not found.");
                    return Ok(membership);
                }

                return Forbid("Access denied.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message });
            }
        }

        // Add a new membership (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddMembership([FromBody] MembershipInputDTO membershipInput)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _membershipService.AddMembership(membershipInput);
            return CreatedAtAction(nameof(GetMembershipById), new { membershipId = membershipInput.RacerId }, membershipInput);
        }

        // Update a membership
        [Authorize(Roles = "Admin,Racer,Supervisor")]
        [HttpPut("{membershipId}")]
        public ActionResult UpdateMembership(int membershipId, [FromBody] MembershipInputDTO membershipInput)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentUserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                var currentUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                if (currentUserRole == "Admin")
                {
                    _membershipService.UpdateMembership(membershipId, membershipInput);
                    return Ok("Membership updated successfully.");
                }

                if (currentUserRole == "Racer" && !_membershipService.IsRacerMembershipOwner(currentUserEmail, membershipId))
                {
                    return Forbid("Access denied: You can only update your own membership.");
                }

                if (currentUserRole == "Supervisor" && !_membershipService.IsSupervisorRelatedToRacer(currentUserEmail, membershipId))
                {
                    return Forbid("Access denied: You can only update memberships for related racers.");
                }

                _membershipService.UpdateMembership(membershipId, membershipInput);
                return Ok("Membership updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "InternalServerError", ErrorMessage = ex.Message });
            }
        }

        // Delete a membership (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{membershipId}")]
        public ActionResult DeleteMembership(int membershipId)
        {
            var success = _membershipService.DeleteMembership(membershipId);
            if (!success)
                return NotFound($"Membership with ID {membershipId} not found.");

            return NoContent();
        }
    }
}
