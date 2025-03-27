using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        public class OrdersUpdate
        {
            public int Id { get; set; }  // Id zorunlu, güncellenen kayıt için gerekli

            public int? CustomerId { get; set; }  // Nullable hale getirdik
            public DateTime? OrderDate { get; set; }  // Nullable hale getirdik
            public decimal? TotalAmount { get; set; }  // Nullable hale getirdik


           
        }

        public class OrdersUpdateStatus
        {
            public int Id { get; set; }  // Id zorunlu, güncellenen kayıt için gerekli
            public string? Status { get; set; }  // Nullable hale getirdik
        }


        public class OrdersDelete
        {
            public int Id { get; set; }
        }
       

    }
}
