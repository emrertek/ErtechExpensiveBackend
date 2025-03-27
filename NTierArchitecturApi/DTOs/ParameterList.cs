using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ParameterList : IEnumerable<Parameters>
    {
        public List<Parameters> Parameters { get; set; }

        public ParameterList()
        {
            Parameters = new List<Parameters>();
        }
        public void Add(string name, object value)
        {
            Parameters.Add(new Parameters { Name = name, Value = value });
        }

        public IEnumerator<Parameters> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Reset()
        {
            Parameters = new List<Parameters>();
        }

    }
}
