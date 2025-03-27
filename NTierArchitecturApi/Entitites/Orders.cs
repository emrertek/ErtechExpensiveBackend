using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entitites
{
    public class Orders : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
    }
}
