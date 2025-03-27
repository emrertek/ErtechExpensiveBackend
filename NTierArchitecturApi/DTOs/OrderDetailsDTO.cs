using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class OrderDetailsDTO
    {
        public class OrderDetailsCreate
        {
            public string? OrderNo { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal SubTotal { get; set; }
        }
        public class OrderDetailsQuery
        {
            public int Id { get; set; }
            public string? OrderNo { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal SubTotal { get; set; }
        }
        public class OrderDetailsUpdate
        {
            public int Id { get; set; }  // Güncellenen kayıt için zorunlu, çünkü ID olmadan hangi kaydın değişeceği bilinemez

            public string? OrderNo { get; set; }  // Nullable hale getirildi
            public int? ProductId { get; set; }  // Nullable hale getirildi
            public int? Quantity { get; set; }  // Nullable hale getirildi
            public decimal? SubTotal { get; set; }  // Nullable hale getirildi

        }
        public class OrderDetailsDelete
        {
            public int Id { get; set; }
        }
    }
}
