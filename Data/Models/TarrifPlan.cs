using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class TarrifPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string tarrif_plan_id { get; set; }
        public int amount { get; set; }
        public string description { get; set; }

        //Account chooses Plan
        [JsonIgnore]
        public List<Account> Account { get; set; }

        //Plan Added By Admin
        public string admin_id { get; set; }
        [JsonIgnore]
        public Admin Admin { get; set; }
    }
}
