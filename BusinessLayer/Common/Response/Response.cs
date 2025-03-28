﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Common.Interface;
using Microsoft.Azure.Amqp.Framing;

namespace BusinessLayer.Common.Response
{
    public class Response<T> : BusinessLayer.Common.Result.Result, IResponse<T>
    {
        public Response(bool success, T data) : base(success)
        {
            Data = data;
        }

        public Response(bool success, string message, T data) : base(success, message)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public Response(string message) : base(default, message)
        {
            Message = message;
        }

        public Response(bool success, string message) : base(success, message)
        {
            Success = success;
            Message = message;
        }

        public T Data { get; set; }
    }
}
