using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Common.Interface;
using DataAccessLayer.DTOs;
using static DataAccessLayer.DTOs.CustomersDTO;

namespace BusinessLayer.Interfaces
{
    public interface ILoginService
    {
      //  IResponse<string> AuthenticateUser(LoginDTO loginDTO);
      IResponse<IEnumerable<string>> AuthenticateUser(LoginAuthDTO model);
    }
}