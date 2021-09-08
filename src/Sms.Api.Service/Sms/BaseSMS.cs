using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Sms
{
    public abstract class BaseSMS
    {
        protected readonly IConfiguration Configuration;

        protected BaseSMS(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool SendSMS(List<string> phones, string content, string signName)
        {
            return SendSMS(string.Join(";", phones), content, signName);
        }

        public abstract bool SendSMS(string phone, string content, string signName);
    }
}
