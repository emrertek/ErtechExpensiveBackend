using BusinessLayer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.PaymentDTO;

namespace BusinessLayer.Interfaces
{
    public interface IPaymentService
    {
        //Create,Delete,getall,getbyId,Update
        IResponse<IEnumerable<PaymentQuery>> ListAll();
        IResponse<IEnumerable<PaymentQuery>> GetByID(int id);
        IResponse<IEnumerable<PaymentQuery>> GetByMethod(string paymentMethod);
        IResponse<string> Create(PaymentCreate model);
        IResponse<string> Update(PaymentUpdate model);
        IResponse<string> Delete(int id);
        
    }
}
