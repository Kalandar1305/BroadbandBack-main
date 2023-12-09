using System.ComponentModel.DataAnnotations;

namespace BroadBandBillingPaymentSystem.Data.ViewModels
{
    public class FeedbackVM
    {
        public string review { get; set; }
        [Required]
        [Range(0,5)]
        public int rating { get; set; }
    }
    public class ReplyToFeedback
    {
        public string feedback_id { get; set; }
        public string reply { get; set; }
    }

    public class FeedbackWithName
    {
         public string feedback_id { get; set; }
        public string review { get; set; }

        public int rating { get; set; }

        public string customer_name { get; set; }
        public DateTime date { get; set; }
    }
}
