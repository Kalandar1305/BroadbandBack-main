using BroadBandBillingPaymentSystem.Data;
using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.Services;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BroadBandBillingPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerService _customerService;
        public CustomerController(CustomerService service)
        {
            _customerService = service;
        }

        [HttpPost("register-user")]
        public IActionResult RegisterUser(CustomerVM customer)
        {
            var exists = this._customerService.CheckUserExists(customer);
            if (exists == null)
            {
                var newCustomer = this._customerService.RegisterUser(customer);
                if (newCustomer == null) return BadRequest();
                return Ok(newCustomer);

            }
            return BadRequest(exists);
        }

        [HttpPut("choose-plan")]
        [Authorize(Roles = "User")]
        public IActionResult ChoosePlan([FromBody] ChoosePlan choosePlan, [FromHeader] string Authorization)
        {
            var _account = this._customerService.choosePlan(choosePlan, Authorization);
            if (_account == null) return BadRequest();
            return Ok(_account);
        }

        [HttpPut("update-profile")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateProfile([FromBody] UpdateProfile customerVM, [FromHeader] string Authorization)
        {
            var error = this._customerService.CheckUpdateError(customerVM, Authorization);
            if(error != null) return BadRequest(error);
            var _customer = this._customerService.UpdateProfile(customerVM, Authorization);
            if (_customer == null) return BadRequest();
            return Ok(_customer);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginVM loginVM)
        {
            var flag = this._customerService.Login(loginVM);
            if (flag.userExists)
            {
                if (flag.response == null)
                {
                    return Unauthorized(new { message = "Incorrect Password" });
                }
                else
                {
                    return Ok(flag.response);
                }
            }
            else return Unauthorized(new { message = "User not found" });
        }

        [HttpGet("get-customer-details")]
        [Authorize(Roles = "User")]
        public IActionResult GetCustomerDetails([FromHeader] string Authorization)
        {
            return Ok(this._customerService.GetCustomerDetails(Authorization));
        }

        [HttpPost("change-password")]
        [Authorize(Roles = "User")]
        public IActionResult ChangePassword([FromBody] ChangePassword password, [FromHeader] string Authorization)
        {
            var result = this._customerService.ChangePassword(password, Authorization);
            if (result.verify) return Ok(new { message = result.message });
            return BadRequest(new { current_password = result.message });
        }

        [HttpGet("get-all-customer")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAllCustomers()
        {
            var customers = this._customerService.GetAllCustomers();
            return Ok(customers);
        }
       
    }
}