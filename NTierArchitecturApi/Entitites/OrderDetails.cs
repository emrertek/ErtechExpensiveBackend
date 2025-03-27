using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entitites
{
    public class OrderDetails : BaseEntity
    {
        public string? OrderNo { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
