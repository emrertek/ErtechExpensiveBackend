using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string email, int customerId, string role);
        string GenerateHashedPassword(string password);
    }       
}
