using Microsoft.Extensions.Configuration;
using Sms.Api.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Sms
{
    public class Test1Sms : BaseSMS
    {
        public Test1Sms(IConfiguration configuration) : base(configuration)
        {
            var config = configuration.Get<SmsConfig>();
            MaxCount = config.Sms.TestSms1Config.MaxCount;
        }
        public override bool SendSMS(string phone, string content, string signName)
        {
            return true;
        }
    }
}
