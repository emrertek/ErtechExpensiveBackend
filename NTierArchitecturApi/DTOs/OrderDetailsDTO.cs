using System;

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
            public string? ProductName { get; set; } // Ek: kullanıcıya görünen isim
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }   // İsteğe bağlı: tekil fiyat
            public decimal SubTotal { get; set; }
        }

        public class OrderDetailsUpdate
        {
            public int Id { get; set; }
            public string? OrderNo { get; set; }
            public int? ProductId { get; set; }
            public int? Quantity { get; set; }
            public decimal? SubTotal { get; set; }
        }

        public class OrderDetailsDelete
        {
            public int Id { get; set; }
        }
    }
}
