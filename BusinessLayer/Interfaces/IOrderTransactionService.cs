using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using DataAccessLayer.DTOs;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IOrderTransactionService
    {
        IResponse<string> CreateCompleteOrder(
            OrdersDTO.OrdersCreate orderDto,
            List<OrderDetailsDTO.OrderDetailsCreate> orderDetails,
            PaymentDTO.PaymentCreate paymentDto,
            CustomerAddressesDTO.CustomerAddressCreate addressDto);
    }
}
