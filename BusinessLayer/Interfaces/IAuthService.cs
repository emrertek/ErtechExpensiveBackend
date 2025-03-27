using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string email, int customerId, bool isAdmin);
        string GenerateHashedPassword(string password);
    }       
}
