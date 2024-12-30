using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Services;
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
        /// Get all admins.
        /// </summary>
        /// <returns>List of AdminOutputDTO</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var admins = _adminService.GetAll();
            return Ok(admins);
        }

        /// <summary>
        /// Get an admin by ID.
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <returns>AdminOutputDTO</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var admin = _adminService.GetById(id);
                return Ok(admin);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Update an admin.
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <param name="adminDto">AdminInputDTO with updated data</param>
        /// <returns>Status message</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AdminInputDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _adminService.Update(id, adminDto);
                return Ok(new { message = "Admin updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Delete an admin.
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <returns>Status message</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _adminService.Delete(id);
                return Ok(new { message = "Admin deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorCode = "NotFound", ErrorMessage = ex.Message });
            }
        }
    }
}
