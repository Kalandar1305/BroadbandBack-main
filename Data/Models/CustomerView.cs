
using Microsoft.EntityFrameworkCore;

namespace BroadBandBillingPaymentSystem.Data.Models
{
    public class CustomerView
    {
        public string? customer_id { get; set; }
        public string? f_name { get; set; }
        public string? l_name { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string?  tarrif_plan_id { get; set; }
        public string? status { get; set; }
        public int? amount { get; set; }

        public string? description { get; set; }

    }

}
