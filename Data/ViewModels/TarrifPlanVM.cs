using System.ComponentModel.DataAnnotations;

namespace BroadBandBillingPaymentSystem.Data.ViewModels
{
    public class TarrifPlanVM
    {
        [Required(ErrorMessage = "Amount is Required")]
        public int amount { get; set; }
        [Required(ErrorMessage = "description is Required")]
        public string description { get; set; }
    }

    public class DeleteTarrifPlan
    { 
        public string tarrif_plan_id { get; set; }
        public string replace_plan_id { get; set; }
    }
    public class UpdateTarrifPlan
    {
        public string tarrif_plan_id { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string admin_id { get; set; }
    
    }
    public class TarrifPlanWithCount
    {
        public string tarrif_plan_id { get; set; }
        public int amount { get; set; }
        public string description { get; set; }

        public int? count { get; set; }
        public string admin_id { get; set; }
    }
}
