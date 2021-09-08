using Microsoft.Extensions.Configuration;
using Sms.Api.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Sms
{
    public class Test2Sms : BaseSMS
    {
        public Test2Sms(IConfiguration configuration) : base(configuration)
        {
            var config = configuration.Get<SmsConfig>();
        }
        public override bool SendSMS(string phone, string content, string signName)
        {
            throw new NotImplementedException();
        }
    }
}
