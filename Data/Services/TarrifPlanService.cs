using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class TarrifPlanService
    {
        private AppDbContext _context;
        private TokenService _tokenService;
        public TarrifPlanService(AppDbContext context, TokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;
        }
        public List<TarrifPlanWithCount> GetTarrifPlans()
        {
            return this._context.TarrifPlan.Select(n => new TarrifPlanWithCount()
            {
                tarrif_plan_id = n.tarrif_plan_id,
                amount = n.amount,
                description = n.description,
                admin_id = n.admin_id,
                count = _context.Account.Where(m => m.tarrif_plan_id == n.tarrif_plan_id).Count()
            })
            .ToList();
        }

        public TarrifPlan AddNewPlan(TarrifPlanVM tarrifPlanVM, string Authorization)
        {

            var admin_id = this._tokenService.GetIdFromToken(Authorization);
            var plan = new TarrifPlan()
            {
                amount = tarrifPlanVM.amount,
                description = tarrifPlanVM.description,
                Admin = this._context.Admin.FirstOrDefault(n => admin_id == n.admin_id)!
            };
            this._context.TarrifPlan.Add(plan);
            this._context.SaveChanges();
            return plan;
        }

        public bool DeletePlan(DeleteTarrifPlan plan)
        {
            var accounts = this._context.Account.Where(n => n.tarrif_plan_id == plan.tarrif_plan_id).ToList();
            foreach (var a in accounts)
            {
                a.tarrif_plan_id = plan.replace_plan_id;
            }
            _context.Account.UpdateRange(accounts);

            var _plan = this._context.TarrifPlan.FirstOrDefault(n => n.tarrif_plan_id == plan.tarrif_plan_id);
            if (_plan != null)
            {
                this._context.TarrifPlan.Remove(_plan);
                this._context.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public bool UpdatePlan(UpdateTarrifPlan plan)
        {
            var _plan = this._context.TarrifPlan.FirstOrDefault(n => n.tarrif_plan_id == plan.tarrif_plan_id)!;
            _plan.amount = plan.amount;
            _plan.description = plan.description;
            this._context.SaveChanges();
            return true;
        }
    }
}
