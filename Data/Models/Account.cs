using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string account_id { get; set; }
        public string status { get; set; }

        //Customer has Account
        [JsonIgnore]
        public Customer Customer { get; set; }

        //Account Choose Plan

        public string? tarrif_plan_id { get; set; }
        [JsonIgnore]
        public TarrifPlan? TarrifPlan { get; set; }

        // Account Generate Bill
        [JsonIgnore]
        public List<Bill> Bill { get; set; }
    }
}
