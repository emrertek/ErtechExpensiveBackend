using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class Parameters
    {
        public string? Name { get; set; }
        public object? Value { get; set; }

        public void AddParameter(string parameterName,object value)
        {
            Name = parameterName;
            Value = value;
        }
    }
}
