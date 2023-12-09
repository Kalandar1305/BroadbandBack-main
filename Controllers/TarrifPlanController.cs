using System.IdentityModel.Tokens.Jwt;
using BroadBandBillingPaymentSystem.Data;
using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.Services;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BroadBandBillingPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarrifPlanController : ControllerBase
    {
        private TarrifPlanService _tarrifPlanService;
        public TarrifPlanController(TarrifPlanService service)
        {
            _tarrifPlanService = service;
        }

        [HttpGet("get-all-plans")]
        public IActionResult GetTarrifPlan()
        {
            var plans = this._tarrifPlanService.GetTarrifPlans().ToList();
            return Ok(plans);
        }

        [HttpPost("add-new-plan")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddNewPlan([FromBody] TarrifPlanVM tarrifPlanVM, [FromHeader]string Authorization)
        {
            var plan = this._tarrifPlanService.AddNewPlan(tarrifPlanVM, Authorization);
            if (plan == null) return BadRequest();
            return Ok(plan);
        }

        [HttpDelete("delete-plan/")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePlan([FromBody]DeleteTarrifPlan tarrif)
        {
            var flag = this._tarrifPlanService.DeletePlan(tarrif);
            if (flag) return Ok(new { message = "Plan Deleted" });
            else return BadRequest();
        }

        [HttpPut("update-plan")]
        [Authorize(Roles="Administrator")]
        public IActionResult UpdatePlan([FromBody]UpdateTarrifPlan plan)
        {
           var flag = this._tarrifPlanService.UpdatePlan(plan);
            if(flag) return Ok();
            return BadRequest();
        }
    }
}
