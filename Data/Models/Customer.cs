using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string customer_id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string address { get; set; }
        [Phone]
        public string phone { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }

        //Customer has Account
        public string account_id { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }

        //Customer gives feedback
        [JsonIgnore]
        public List<Feedback> Feedback { get; set; }

        //Customer pays Bill
        [JsonIgnore]
        public List<Bill> Bill { get; set; }
    }

    public class ChangePassword
    {
        public string current_password { get; set; }
        public string new_password { get; set; }
    }
}
