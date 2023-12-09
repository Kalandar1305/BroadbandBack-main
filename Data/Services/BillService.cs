using System.Collections.Generic;
using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.ViewModels;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class BillService
    {
        private AppDbContext _context;
        private TokenService _tokenService;
        public BillService(AppDbContext context, TokenService tokenService)
        {
            this._context = context;
            _tokenService = tokenService;
        }

        public List<BillVM> GetBillsByCustomerId(string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var customer = this._context.Customer.FirstOrDefault(n => n.customer_id == id)!;
            var bills = this._context.Bill.Where(n => n.account_id == customer.account_id).Select(n => new BillVM()
            {
                bill_id = n.bill_id,
                amount = n.amount,
                due_date = n.due_date,
                bill_date = n.bill_date,
                payment_date = n.payment_date,
                payment_mode = n.payment_mode
            }).OrderByDescending(n => n.bill_date).ToList();
            return bills;
        }

        public List<BillVM> GetPendingBills(string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var customer = this._context.Customer.FirstOrDefault(n => n.customer_id == id)!;
            var bills = this._context.Bill.Where(n => n.account_id == customer.account_id && n.payment_date == null).Select(n => new BillVM()
            {
                bill_id = n.bill_id,
                amount = n.amount,
                due_date = n.due_date,
                bill_date = n.bill_date,
                payment_date = n.payment_date,
                payment_mode = n.payment_mode
            }).OrderByDescending(n => n.bill_date).ToList();
            return bills;
        }

        public List<BillVM> GetAllPendingBills()
        {
            var bills = this._context.Bill.Where(n => n.payment_date == null).Select(n => new BillVM()
            {
                bill_id = n.bill_id,
                amount = n.amount,
                due_date = n.due_date,
                bill_date = n.bill_date,
                payment_date = n.payment_date,
                payment_mode = n.payment_mode
            }).OrderByDescending(n => n.bill_date).ToList();
            return bills;
        }
        public BillVM PayBill(PayBillVM payBillVM, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var bill = this._context.Bill.FirstOrDefault(n => n.bill_id == payBillVM.bill_id);
            bill!.payment_date = DateTime.Now;
            bill.payment_mode = payBillVM.payment_mode;
            bill.customer_id = id;
            this._context.Bill.Update(bill);
            this._context.SaveChanges();
            return new BillVM()
            {
                bill_id = bill.bill_id,
                amount = bill.amount,
                due_date = bill.due_date,
                bill_date = bill.bill_date,
                payment_date = bill.payment_date,
                payment_mode = bill.payment_mode
            };
        }

        public Boolean GenerateBills()
        {
            var bill = this._context.Bill.FirstOrDefault(n => n.bill_date.Month == DateTime.Now.Month && n.bill_date.Year == DateTime.Now.Year);
            if (bill != null) return false;

            List<Bill> newBills = new List<Bill>();
            var accounts = this._context.Account.ToList();
            foreach (var account in accounts)
            {
                if (account.tarrif_plan_id != null)
                {
                    Bill _bill = new Bill()
                    {
                        account_id = account.account_id,
                        amount = this._context.TarrifPlan.FirstOrDefault(n => n.tarrif_plan_id == account.tarrif_plan_id)!.amount,
                        bill_date = DateTime.Now,
                        due_date = DateTime.Now.AddDays(10),
                        payment_date = null,
                        payment_mode = null
                    };
                    newBills.Add(_bill);
                }
            }
            this._context.Bill.AddRange(newBills);
            this._context.SaveChanges();
            return true;
        }

    }
}