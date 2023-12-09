using BroadBandBillingPaymentSystem.Data.Models;

namespace BroadBandBillingPaymentSystem.Data.ViewModels
{
    public class HomePageVM
    {
        public List<TarrifPlanWithCount> popular_plans { get; set; }
        public List<FeedbackWithName> top_reviews { get; set; }
    }
}