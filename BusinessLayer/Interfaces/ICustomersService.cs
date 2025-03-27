using BusinessLayer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.CustomersDTO;

namespace BusinessLayer.Interfaces
{
    public interface ICustomersService
    {
        IResponse<IEnumerable<CustomerQuery>> ListAll();
        IResponse<IEnumerable<CustomerQuery>> FindById(int id);
        IResponse<string> Update(CustomerUpdate model);
        IResponse<string> UpdatePassword(int customerId, string password);
        IResponse<string> Create(CustomerCreate model);
        IResponse<string> Delete(int id);
    }
}
