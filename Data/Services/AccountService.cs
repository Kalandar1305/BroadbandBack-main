using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class AccountService
    {
        private AppDbContext _context;
        public AccountService(AppDbContext context)
        {
            this._context = context;
        }

        public Account CreateAccount()
        {
            var newAccount = new Account()
            {
                status = "idle"
            };

            this._context.Account.Add(newAccount);
            this._context.SaveChanges();
            return newAccount;
        }

        public Account? choosePlan(string account_id, string plan_id)
        {
            var _account = this._context.Account.FirstOrDefault(n => n.account_id == account_id);
            if (null == _account) return null;
            _context.Database.ExecuteSqlRaw(String.Format("exec chooseplan '{0}', '{1}'",plan_id, account_id));
            this._context.SaveChanges();
            return _account;
        }

        public Account? GetAccountDetails(string account_id)
        {
            return this._context.Account.FirstOrDefault(n => n.account_id == account_id);
        }
    }
}
