using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class PaymentDTO
    {
        public class PaymentCreate
        {
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Amount { get; set; }
        }
        public class PaymentQuery
        {
            public int Id { get; set; }
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Amount { get; set; }
        }
        public class PaymentUpdate
        {
            public int Id { get; set; }
            public string? PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Amount { get; set; }
        }
        public class PaymentDelete
        {
            public int Id { get; set; }
        }
    }
}
