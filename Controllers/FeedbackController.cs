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
    public class FeedbackController : ControllerBase
    {
        private FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            this._feedbackService = feedbackService;
        }

        [HttpPost("add-feedback")]
        [Authorize(Roles = "User")]
        public IActionResult AddFeedback([FromBody] FeedbackVM feedbackVM, [FromHeader] string Authorization)
        {
            var _feedback = this._feedbackService.AddFeedback(feedbackVM, Authorization);
            if (feedbackVM != null) return Ok(_feedback);
            return BadRequest();

        }

        [HttpPut("reply-feedback")]
        [Authorize(Roles = "Administrator")]
        public IActionResult ReplyToFeedback([FromBody] ReplyToFeedback replyToFeedback, [FromHeader] string Authorization)
        {
            var feedback = this._feedbackService.ReplyToFeedback(replyToFeedback, Authorization);
            if (feedback == null) return BadRequest();
            return Ok(feedback);
        }

        [HttpGet("get-all-feedbacks-user")]
        [Authorize(Roles = "User")]
        public IActionResult GetFeedbacksofUser([FromHeader] string Authorization)
        {
            return Ok(this._feedbackService.GetAllFeedbacksOfUser(Authorization));
        }
        [HttpGet("get-all-feedbacks")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetFeedbacks()
        {
            return Ok(this._feedbackService.GetAllFeedbacks());
        }

        [HttpGet("get-all-unanswered")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetFeedbacksUnAnswered()
        {
            return Ok(this._feedbackService.GetAllFeedbacksUnanswered());
        }

        [HttpGet("get-all-answered")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAllAnswered()
        {
            return Ok(this._feedbackService.GetAllAnswered());
        }
    }
}
