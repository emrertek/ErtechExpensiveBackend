using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateHashedPassword(string password)
        {
            // string secretPasswordKey = _configuration.GetValue<string>("AppSettings:PasswordKey");

            string secretPasswordKey = "ASDHJASFHAJSDHASJDKSA";
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretPasswordKey);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = hmac.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashedBytes);
            }
        }

        public string GenerateToken(string email, int customerId,bool isAdmin)
        {
            //string secret = _configuration.GetValue<string>("AppSettings:SecretKey");

            string secret = "SASHASKJDASHFJASHKD1234567890123456";
            byte[] key = Encoding.UTF8.GetBytes(secret);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("Email", email));
            claims.Add(new Claim("CustomerId", customerId.ToString()));
            claims.Add(new Claim("IsAdmin", isAdmin.ToString()));


            JwtSecurityToken securityToken =
                new JwtSecurityToken(
                    signingCredentials: credentials,
                    claims: claims,
                    expires: DateTime.Now.AddDays(7));

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
