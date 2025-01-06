using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Services;
using Microsoft.AspNetCore.Authorization; // For role-based authorization
using Microsoft.AspNetCore.Mvc;

namespace KartingSystemSimulation.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService; // Inject AdminService
        }

        /// <summary>
        /// Get all admins (Admin only).
        /// </summary>
        /// <returns>List of AdminOutputDTO</returns>
        [Authorize(Roles = "Admin")] // Restrict access to Admin role only
        [HttpGet]
        public IActionResult GetAll()
        {
            // Retrieve all admins from the service layer
            var admins = _adminService.GetAll();

            // Return the list of admins
            return Ok(admins);
        }

        /// <summary>
        /// Get an admin by ID (Admin only).
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <returns>AdminOutputDTO</returns>
        [Authorize(Roles = "Admin")] // Restrict access to Admin role only
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                // Retrieve the admin with the specified ID from the service layer
                var admin = _adminService.GetById(id);

                // Return the admin details
                return Ok(admin);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 if the admin is not found
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Update an admin. Allows the current logged-in admin to update their own details.
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <param name="adminDto">AdminInputDTO with updated data</param>
        /// <returns>Status message</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AdminInputDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Get the email of the currently logged-in admin
                var currentAdminEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(currentAdminEmail))
                {
                    return Unauthorized(new { message = "Unauthorized: Unable to retrieve current admin email." });
                }

                // Check if the admin being updated is the logged-in admin
                var admin = _adminService.GetById(id);
                if (admin == null)
                {
                    return NotFound(new { ErrorCode = "NotFound", ErrorMessage = "Admin not found." });
                }

                if (!admin.Email.Equals(currentAdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid("You are only allowed to update your own details.");
                }

                // Perform the update
                _adminService.Update(id, adminDto);
                return Ok(new { message = "Admin updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

        /// <summary>
        /// Delete an admin. Prevents the currently logged-in admin from deleting themselves.
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <returns>Status message</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Get the email of the currently logged-in admin
                var currentAdminEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(currentAdminEmail))
                {
                    return Unauthorized(new { message = "Unauthorized: Unable to retrieve current admin email." });
                }

                // Get the admin to be deleted
                var adminToDelete = _adminService.GetById(id);
                if (adminToDelete == null)
                {
                    return NotFound(new { ErrorCode = "NotFound", ErrorMessage = "Admin not found." });
                }

                // Prevent self-deletion
                if (adminToDelete.Email.Equals(currentAdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid("You cannot delete yourself.");
                }

                // Perform the deletion
                _adminService.Delete(id);
                return Ok(new { message = "Admin deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

    }
}
