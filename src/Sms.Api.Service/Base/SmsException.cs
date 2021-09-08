using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Base
{
    public class SmsException : ApplicationException
    {
        public SmsException(string msg) : base(msg)
        {
        }
    }
}
