using System.Linq;
using System.Security;
using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class AdminService
    {
        private AppDbContext _context;
        private TokenService _tokenService;
        public AdminService(AppDbContext context, TokenService token)
        {
            this._context = context;
            this._tokenService = token;
        }

        public ViewAdmin? AddAdmin(AdminVM admin)
        {
            var exists = this._context.Admin.FirstOrDefault(n => n.email == admin.email);
            if (exists != null) return null;
            var hash = BCrypt.Net.BCrypt.HashPassword(admin.password, BCrypt.Net.BCrypt.GenerateSalt(12));
            var _admin = new Admin()
            {
                email = admin.email,
                password = hash,
            };
            this._context.Add(_admin);
            this._context.SaveChanges();
            return new ViewAdmin()
            {
                admin_id = _admin.admin_id,
                email = admin.email
            };
        }

        public Boolean DeleteAdmin(string admin_id)
        {

            var _admin = this._context.Admin.FirstOrDefault(n => n.admin_id == admin_id);
            if (_admin == null)
                return false;
            this._context.Admin.Remove(_admin);
            return true;
        }

        public LoginResponse? Login(LoginVM login)
        {
            this.GetDashboardData();

            var admin = this._context.Admin.FirstOrDefault(n => n.email == login.email);
            if (admin == null) return null;
            Boolean verified = this._tokenService.VerifyPassword(login.password, admin.password);
            if (verified)
            {
                var token = this._tokenService.CreateAdminToken(admin);
                return new LoginResponse()
                {
                    message = "Login Success",
                    token = token,
                };
            }
            return null;
        }
        public (Boolean verify, string message) ChangePassword(ChangePassword password, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var admin = this._context.Admin.FirstOrDefault(n => n.admin_id == id);
            var verify = this._tokenService.VerifyPassword(password.current_password, admin!.password);
            if (verify)
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(password.new_password, BCrypt.Net.BCrypt.GenerateSalt(12));
                admin.password = hash;
                this._context.SaveChanges();
                return (true, "Password Changed");
            }
            else
                return (false, "Wrong Password");
        }

        public List<Admin> GetAllAdmins()
        {
            this.GetDashboardData();
            return this._context.Admin.ToList();
        }

        public AdminDashboard GetDashboardData()
        {
            var no_of_users = _context.Customer.Count();
            var no_of_plans = _context.TarrifPlan.Count();
            var no_of_feedbacks = _context.Feedback.Count();
            var highest_rating = 0;
            var average_rating = 0.0;
            try
            {
                highest_rating = _context.Feedback.Max(n => n.rating);
                average_rating = _context.Feedback.Average(n => n.rating);

            }
            catch (Exception e)
            {
                highest_rating = 0;
                average_rating = 0;
            }
            TarrifPlan? most_used_plan = null;
            try
            {
                most_used_plan = _context.Set<TarrifPlan>().FromSqlRaw("exec mostusedplan").ToList()[0];
            }
            catch (Exception e)
            {
                most_used_plan = null;

            }
            return new AdminDashboard()
            {
                no_of_users = no_of_users,
                no_of_plans = no_of_plans,
                no_of_feedbacks = no_of_feedbacks,
                highest_rating = highest_rating,
                average_rating = (float)average_rating,
                most_used_plan = most_used_plan
            };
        }
    }
}