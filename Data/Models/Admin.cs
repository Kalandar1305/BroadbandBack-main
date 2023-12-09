using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string admin_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        //Plan Added By Admin
        public List<TarrifPlan> TarrifPlan { get; set; }

        //Feedback replied by admin
        public List<Feedback> Feedback { get; set; }
    }
}
