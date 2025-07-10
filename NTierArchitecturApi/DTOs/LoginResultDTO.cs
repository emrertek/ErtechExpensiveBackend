using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class LoginResultDTO
    {
        public string Token { get; set; }
        public string Role { get; set; } // Default olarak 'User'
        public string Email { get; set; }
        public int CustomerId { get; set; }
    }
}