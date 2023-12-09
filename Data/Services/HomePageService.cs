using BroadBandBillingPaymentSystem.Data.ViewModels;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class HomePageService
    {
        private AppDbContext _context;
        public HomePageService(AppDbContext dbcontext, TarrifPlanService service)
        {
            this._context = dbcontext;
        }

        public HomePageVM GetLandingPageData()
        {
            var popular_plans = _context.TarrifPlan.Select(n => new TarrifPlanWithCount()
            {
                tarrif_plan_id = n.tarrif_plan_id,
                amount = n.amount,
                description = n.description,
                admin_id = n.admin_id,
                count = _context.Account.Where(m => m.tarrif_plan_id == n.tarrif_plan_id).Count()
            }).OrderByDescending(n => n.count).Take(5).ToList();

          var top_reviews = _context.Feedback.Join(_context.Customer, f => f.customer_id, c => c.customer_id, (f, c) => new FeedbackWithName()
            {
                feedback_id = f.feedback_id,
                review = f.review,
                rating = f.rating,
                customer_name = c.f_name + " " + c.l_name,
                date = f.date
            }).OrderByDescending(n => n.rating).Take(5).ToList();
            return new HomePageVM()
            {
                popular_plans = popular_plans,
                top_reviews = top_reviews
            };
        }
    }
}