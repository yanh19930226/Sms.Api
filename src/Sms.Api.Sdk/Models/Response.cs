using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sms.Api.Sdk.Models
{
    public class Response<T> : Response
    {
        public T Body { get; set; }
    }

    public class Response
    {
        public string Message { get; set; }

        public HttpStatusCode StateCode { get; set; }

        public bool IsSuccess => StateCode == HttpStatusCode.OK;

    }
}
