using BroadBandBillingPaymentSystem.Data.Services;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BroadBandBillingPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private BillService _billService;

        public BillController(BillService billService)
        {
            this._billService = billService;
        }

        [HttpGet("get-bills")]
        [Authorize(Roles = "User,Administrator")]
        public IActionResult GetBillsByCustomerId([FromHeader] string Authorization)
        {
            var bills = this._billService.GetBillsByCustomerId(Authorization);
            return Ok(bills);
        }

        [HttpGet("get-pending-bills")]
        [Authorize(Roles = "User,Administrator")]
        public IActionResult GetPendingBills([FromHeader] string Authorization)
        {
            var bills = this._billService.GetPendingBills(Authorization);
            return Ok(bills);
        }

        [HttpPut("pay-bill")]
        [Authorize(Roles = "User")]
        public IActionResult PayBill([FromBody] PayBillVM payBillVM, [FromHeader] string Authorization)
        {
            var bill = this._billService.PayBill(payBillVM, Authorization);
            return Ok(bill);
        }

        [HttpPost("generate-bill")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GenerateBills()
        {
            var flag = this._billService.GenerateBills();
            if (flag) return Ok(new { message = String.Format("Bills for {0} {1} Generated Successfully", DateTime.Now.Month, DateTime.Now.Year) });
            else
                return BadRequest(new { message = String.Format("Bills for {0} {1} Already Generated", DateTime.Now.Month, DateTime.Now.Year) });
        }

        [HttpGet("get-all-pending-bills")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAllPendingBills()
        {
            return Ok(this._billService.GetAllPendingBills());
        }
    }
}
