using BusinessLayer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.OrderDetailsDTO;
using static DataAccessLayer.DTOs.OrdersDTO;

namespace BusinessLayer.Interfaces
{
    public interface IOrdersService
    {
        IResponse<IEnumerable<OrdersQuery>> ListAll();
        IResponse<IEnumerable<OrdersQuery>> GetById(int id);
        IResponse<string> Update(OrdersUpdate model);
        IResponse<string> UpdateStatus(OrdersUpdateStatus model);

        IResponse<string> Create(OrdersCreate model);
        IResponse<string> Delete(int id);
        
    }
}
