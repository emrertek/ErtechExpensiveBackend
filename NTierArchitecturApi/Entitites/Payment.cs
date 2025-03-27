using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entitites
{
    public class Payment : BaseEntity
    {
        public string? PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
