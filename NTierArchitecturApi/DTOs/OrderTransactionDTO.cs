using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class OrderTransactionDTO
    {
        public class Create
        {
            public OrdersDTO.OrdersCreate Order { get; set; }
            public List<OrderDetailsDTO.OrderDetailsCreate> OrderDetails { get; set; }
            public PaymentDTO.PaymentCreate Payment { get; set; }
            public CustomerAddressesDTO.CustomerAddressCreate Address { get; set; }
        }
    }
}
