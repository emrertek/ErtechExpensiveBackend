using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ProductsDTO
    {
        public class ProductsCreate
        {
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string? Category { get; set; }
        }
        public class ProductsQuery
        {
            public int ProductID { get; set; }
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string? Category { get; set; }
        }
        public class ProductsUpdate
        {
            public int Id { get; set; }
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string? Category { get; set; }
        }
        public class ProductsDelete
        {
            public int Id { get; set; }
        }


    }
}
