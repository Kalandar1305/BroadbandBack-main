using System.ComponentModel.DataAnnotations;
using BroadBandBillingPaymentSystem.Data.Models;

namespace BroadBandBillingPaymentSystem.Data.ViewModels
{
    public class AdminVM
    {
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address Not Valid")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password, ErrorMessage = "Password does not meet requirements")]
        public string password { get; set; }
    }

    public class DeleteAdmin
    {
        public string admin_id { get; set; }
    }

    public class ViewAdmin
    {
        public string admin_id { get; set; }
        public string email { get; set; }
    }

    public class AdminDashboard {
        public int no_of_users { get; set; }
        public int no_of_plans { get; set; }
        public int no_of_feedbacks { get; set; }
        public int highest_rating { get; set; }
        public float average_rating { get; set; }
        public TarrifPlan? most_used_plan { get; set; }
    }


}
