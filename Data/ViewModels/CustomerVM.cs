using System.ComponentModel.DataAnnotations;

namespace BroadBandBillingPaymentSystem.Data.ViewModels
{
    public class CustomerVM
    {
        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage = "First Name can contain only letters")]
        public string f_name { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage = "Last Name can contain only letters")]
        public string l_name { get; set; }
        public string address { get; set; }
        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone(ErrorMessage = "Phone Number Not Valid")]
        [MaxLength(10, ErrorMessage = "Phone Number not valid")]
        [MinLength(10, ErrorMessage = "Phone Number not valid")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address Not Valid")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Email address not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password, ErrorMessage = "Password does not meet requirements")]
        [MinLength(8, ErrorMessage = "Password should contain minimumn 8 characters")]
        [MaxLength(20, ErrorMessage = "Password should not contain more than 20 characters")]
        public string password { get; set; }
    }

    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }

    public class LoginResponse 
    {
        public string message { get; set; }
        public string token { get; set; }
    }

    public class CustomerDetails
    {
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public string status { get; set; }

        public int? amount { get; set; }
        public string? description { get; set; }
        public string? tarrif_plan_id { get; set; }
    }

    public class UpdateProfile
    {
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    
}
