using BusinessLayer.Common.Interface;
using static DataAccessLayer.DTOs.CustomerAddressesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICustomerAddressesService
    {
        IResponse<string> Create(AddressCreate model);
        IResponse<string> Update(AddressUpdate model);
        IResponse<string> Delete(int id);
        IResponse<IEnumerable<AddressQuery>> ListAll();
        IResponse<IEnumerable<AddressQuery>> FindByCustomerId(int customerId);
    }
}
