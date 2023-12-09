using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.ViewModels;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    
    public class FeedbackService
    {
        private TokenService _tokenService;
        private AppDbContext _context;
        public FeedbackService(AppDbContext context,TokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;
        }
        public Feedback AddFeedback(FeedbackVM feedbackVM, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var feedback = new Feedback()
            {
                review = feedbackVM.review,
                rating = feedbackVM.rating,
                Customer = this._context.Customer.FirstOrDefault(n => n.customer_id == id)!,
                date = DateTime.Now
            };
            this._context.Feedback.Add(feedback);
            this._context.SaveChanges();
            return feedback;
        }

        public Feedback? ReplyToFeedback(ReplyToFeedback reply, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var _feedback = this._context.Feedback.FirstOrDefault(n => n.feedback_id == reply.feedback_id);
            if (_feedback == null)
                return null;
            _feedback.reply = reply.reply;
            _feedback.reply_date = DateTime.Now;
            _feedback.Admin = this._context.Admin.FirstOrDefault(n => n.admin_id == id);
            this._context.SaveChanges();
            return _feedback;
        }

        public List<Feedback> GetAllFeedbacksOfUser(string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var feedbacks = this._context.Feedback.Where(n => n.customer_id == id).ToList();
            return feedbacks;
        }

        public List<Feedback> GetAllFeedbacks()
        {
            var feedbacks = this._context.Feedback.ToList();
            return feedbacks;
        }

          public List<Feedback> GetAllFeedbacksUnanswered()
        {
            var feedbacks = this._context.Feedback.Where(n => n.reply == null).ToList();
            return feedbacks;
        }

         public List<Feedback> GetAllAnswered()
        {
            var feedbacks = this._context.Feedback.Where(n => n.reply != null).ToList();
            return feedbacks;
        }
    }
 
}
