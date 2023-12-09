using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BroadBandBillingPaymentSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BroadBandBillingPaymentSystem.Data.Services
{
    public class TokenService : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            Console.WriteLine(config["TokenKey"]);
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateCustomerToken(Customer customer)
        {
            var claims = new List<Claim>(){
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Name, customer.f_name),
                new Claim(ClaimTypes.Role, Roles.User.ToString()),
                new Claim("id", customer.customer_id)
            };

            return GenerateToken(claims);
        }

        public string CreateAdminToken(Admin admin)
        {
            var claims = new List<Claim>(){
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Name, admin.email),
                new Claim(ClaimTypes.Role, Roles.Administrator.ToString()),
                new Claim("id", admin.admin_id)
            };

            return GenerateToken(claims);
        }

        public string GetIdFromToken(string BearerToken)
        {
            var token = BearerToken.Split(" ")[1];
            var decoded = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = decoded.Claims.First(claim => claim.Type == "id").Value;
            return id;
        }
        private string GenerateToken(List<Claim> claims)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Boolean VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

    }
}