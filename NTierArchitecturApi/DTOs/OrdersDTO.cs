using System;
using System.Collections.Generic;

namespace DataAccessLayer.DTOs
{
    public class OrdersDTO
    {
        public class OrdersCreate
        {
            public int CustomerId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string? Status { get; set; }
        }

        public class OrdersQuery
        {
            public int Id { get; set; }
            public int CustomerId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string? Status { get; set; }
            public PaymentDTO.PaymentQuery? Payment { get; set; }
            public CustomerAddressesDTO.CustomerAddressQuery? Address { get; set; }
            public List<OrderDetailsDTO.OrderDetailsQuery>? OrderDetails { get; set; }
        }

        public class OrdersUpdate
        {
            public int Id { get; set; }
            public int? CustomerId { get; set; }
            public DateTime? OrderDate { get; set; }
            public decimal? TotalAmount { get; set; }
            public string? Status { get; set; }
        }

        public class OrdersUpdateStatus
        {
            public int Id { get; set; }
            public string? Status { get; set; }
        }

        public class OrdersDelete
        {
            public int Id { get; set; }
        }
    }
}
