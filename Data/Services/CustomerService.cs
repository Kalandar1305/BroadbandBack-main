using BCrypt.Net;
using BroadBandBillingPaymentSystem.Data.Models;
using BroadBandBillingPaymentSystem.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class CustomerService
    {
        private AppDbContext _context;
        private AccountService _accountService;
        private TokenService _tokenService;

        public CustomerService(AppDbContext context, AccountService accountService, TokenService token)
        {
            this._accountService = accountService;
            this._context = context;
            this._tokenService = token;
        }

        public Customer? RegisterUser(CustomerVM customer)
        {
            var _account = this._accountService.CreateAccount();
            if (customer == null) return null;
            var hash = BCrypt.Net.BCrypt.HashPassword(customer.password, BCrypt.Net.BCrypt.GenerateSalt(12));
            var _customer = new Customer()
            {
                f_name = customer.f_name,
                l_name = customer.l_name,
                address = customer.address,
                phone = customer.phone,
                email = customer.email,
                password = hash,
                account_id = _account.account_id,
            };

            this._context.Customer.Add(_customer);
            this._context.SaveChanges();
            return _customer;

        }

        public Account? choosePlan(ChoosePlan choosePlan, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var customer = this._context.Customer.FirstOrDefault(n => n.customer_id == id);
            if (customer == null) return null;
            return this._accountService.choosePlan(customer.account_id, choosePlan.plan_id);
        }

        public Customer? UpdateProfile(UpdateProfile customer, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var _customer = this._context.Customer.FirstOrDefault(n => n.customer_id == id);
            if (_customer != null)
            {
                _customer.f_name = customer.f_name;
                _customer.l_name = customer.l_name;
                _customer.address = customer.address;
                _customer.phone = customer.phone;
                _customer.email = customer.email;
            }

            this._context.SaveChanges();
            return _customer;
        }

        public (Boolean userExists, LoginResponse? response) Login(LoginVM login)
        {
            var customer = this._context.Customer.FirstOrDefault(n => n.email == login.email);
            if (customer == null) return (false, null);
            Boolean verified = this._tokenService.VerifyPassword(login.password, customer.password);
            if (verified)
            {
                var token = this._tokenService.CreateCustomerToken(customer);
                return (true, new LoginResponse()
                {
                    message = "Login Success",
                    token = token
                });
            }
            return (true, null);
        }

        public List<Customer> GetAllCustomers()
        {
            return this._context.Customer.ToList();
        }
        public RegisterError? CheckUserExists(CustomerVM customer)
        {
            var email = this._context.Customer.FirstOrDefault(n => n.email == customer.email);
            var phone = this._context.Customer.FirstOrDefault(n => n.phone == customer.phone);
            if (email == null && phone == null) return null;
            return new RegisterError()
            {
                email = email == null ? new string[] { "" } : new string[] { "Email Already in use" },
                phone = phone == null ? new string[] { "" } : new string[] { "Phone Already in use" }
            };
        }

        public RegisterError? CheckUpdateError(UpdateProfile customer, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var email = this._context.Customer.Where(n => n.email == customer.email).Select(n=>n).ToList();
            var phone = this._context.Customer.Where(n => n.phone == customer.phone).Select(n=>n).ToList();
            var error = new RegisterError();
            var flag = false;
            foreach(var e in email){
                if(e.customer_id != id)
                {
                    error.email = new string[] { "Email Already in use by other account" };
                    flag = true;
                    break;
                }
            }

            foreach(var e in email){
                if(e.customer_id != id)
                {
                    error.phone = new string[] { "Phone Already in use by other account" };
                    flag = true;
                    break;
                }
            }

            if(flag){
                return error;
            }
            return null;

        }

        public (Boolean verify, string message) ChangePassword(ChangePassword password, string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var customer = this._context.Customer.FirstOrDefault(n => n.customer_id == id);
            var verify = this._tokenService.VerifyPassword(password.current_password, customer!.password);
            if (verify)
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(password.new_password, BCrypt.Net.BCrypt.GenerateSalt(12));
                customer.password = hash;
                this._context.SaveChanges();
                return (true, "Password Changed");
            }
            else
                return (false, "Wrong Password");

        }
        public CustomerDetails GetCustomerDetails(string Authorization)
        {
            var id = this._tokenService.GetIdFromToken(Authorization);
            var vewiew = _context.CustomerView.ToList();
            var customer = _context.CustomerView.FirstOrDefault(n => n.customer_id == id )!;
                return new CustomerDetails()
                {
                    f_name = customer.f_name,
                    l_name = customer.l_name,
                    address = customer.address,
                    phone = customer.phone,
                    email = customer.email,
                    status = customer.status,
                    amount = customer.amount,
                    description = customer.description,
                    tarrif_plan_id = customer.tarrif_plan_id
                };
        }
    }
}
