using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CustomersDTO
    {
        public class CustomerCreate()
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }

            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Password { get; set; }

            public bool IsAdmin { get; set; }

        }
        public class CustomerQuery()
        {
            public int Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Password { get; set; }
            public bool IsAdmin { get; set; }
        }


        public class CustomerUpdate()
        {
            public int Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Password { get; set; }
            public bool IsAdmin { get; set; }

        }
        public class CustomerUpdatePassword()
        {
            public int Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Password { get; set; }
            public bool IsAdmin { get; set; }

        }


        public class CustomerDelete()
        {
            public int Id { get; set; }
        }
    }
}
