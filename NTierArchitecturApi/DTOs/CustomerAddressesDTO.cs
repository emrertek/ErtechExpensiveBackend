using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CustomerAddressesDTO
    {
        public class AddressCreate
        {
            public int CustomerID { get; set; }
            public string? AddressLine { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
           // public string State { get; set; }
        }

        public class AddressQuery
        {
            public int Id { get; set; }
            public int CustomerID { get; set; }
            public string? AddressLine { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? State { get; set; }
        }

        public class AddressUpdate
        {
            public int   AddressId { get; set; }
            public string? AddressLine { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? State { get; set; }
        }


        public class CustomerAddressCreate
        {
            public int CustomerId { get; set; }
            public string? AddressLine { get; set; }
            public string? City { get; set; }
            public string? PostalCode { get; set; }
            public string? Country { get; set; }
        }


        public class CustomerAddressQuery
        {
            public int Id { get; set; }
            public int CustomerID { get; set; }
            public string? AddressLine { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? State { get; set; }
        }

    }
}
