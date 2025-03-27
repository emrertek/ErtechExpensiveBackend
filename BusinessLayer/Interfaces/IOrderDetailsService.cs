using BusinessLayer.Common.Interface;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.OrderDetailsDTO;

namespace BusinessLayer.Interfaces
{
    public interface IOrderDetailsService
    {
        IResponse<IEnumerable<OrderDetailsQuery>>ListAll();
        IResponse<IEnumerable<OrderDetailsQuery>> GetById(int id);
        IResponse<IEnumerable<OrderDetailsQuery>> GetByOrderNo(string orderNo);
        IResponse<string> Update(OrderDetailsUpdate model);
        IResponse<string> Create(OrderDetailsCreate model);
        IResponse<string> Delete(int id);
    }
}
