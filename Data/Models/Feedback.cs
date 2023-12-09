using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string feedback_id { get; set; }
        public string review { get; set; }

        public int rating { get; set; }
        public string? reply { get; set; }
        
        public DateTime date { get; set; }
        public DateTime? reply_date { get; set; }

        //Customer gives feedback
        public string customer_id { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }



        //Feedback replied by Admin
        public string? admin_id { get; set; }
        [JsonIgnore]
        public Admin? Admin { get; set; }
        
    }
}
