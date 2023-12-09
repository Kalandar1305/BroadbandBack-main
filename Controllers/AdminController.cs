using BroadBandBillingPaymentSystem.Data.Services;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using BroadBandBillingPaymentSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BroadBandBillingPaymentSystem.Data.Models;

namespace BroadBandBillingPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            this._adminService = adminService;
        }

        [HttpPost("add-admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddAdmin(AdminVM adminVM)
        {
            var admin = this._adminService.AddAdmin(adminVM);
            if (admin == null) return BadRequest(new { message = "Email is already in use" });
            return Ok(admin);
        }

        [HttpDelete("delete-admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteAdmin([FromBody] DeleteAdmin admin)
        {
            var flag = this._adminService.DeleteAdmin(admin.admin_id);
            if (flag) return Ok(new { message = "Admin Deleted" });
            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginVM loginVM)
        {
            var response = this._adminService.Login(loginVM);
            if (response == null) return Unauthorized(new { message = "Incorrect Username/Password" });
            return Ok(response);
        }

        [HttpGet("get-all-admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAllAdmin()
        {
            var admins = this._adminService.GetAllAdmins();
            return Ok(admins);
        }

        [HttpPost("change-password")]
        [Authorize(Roles = "Administrator")]
        public IActionResult ChangePassword([FromBody] ChangePassword password, [FromHeader] string Authorization)
        {
            var result = this._adminService.ChangePassword(password, Authorization);
            if (result.verify) return Ok(new { message = result.message });
            return BadRequest(new { current_password = result.message });
        }

        [HttpGet("get-admin-dashboard")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAdminDashboard()
        {
            var dashboard = this._adminService.GetDashboardData();
            return Ok(dashboard);
        }
    }
}
