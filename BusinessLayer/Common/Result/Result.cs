using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Common.Result
{
    public class Result
    {
        public Result(bool success)
        {
            Success = success;
        }
        public Result(string message)
        {
            Message = message;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
