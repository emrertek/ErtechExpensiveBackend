using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class LoginDTO
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class LoginAuthDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
