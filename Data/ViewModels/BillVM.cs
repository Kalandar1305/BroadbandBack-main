namespace BroadBandBillingPaymentSystem.Data.ViewModels
{
    public class BillVM
    {
        public string bill_id { get; set; }
        public int amount { get; set; }
        public DateTime due_date { get; set; }
        public DateTime bill_date { get; set; }
        public string? payment_mode { get; set; }
        public DateTime? payment_date { get; set; }
    }

}
