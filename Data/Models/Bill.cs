using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string bill_id { get; set; }
        public int amount { get; set; }
        public DateTime due_date { get; set; }
        public DateTime bill_date { get; set; }
        public string? payment_mode { get; set; }
        public DateTime? payment_date { get; set; }

        //Customer pays bill
        public string? customer_id { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }

        //Account Generates Bill
        public string account_id { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
    }
}
