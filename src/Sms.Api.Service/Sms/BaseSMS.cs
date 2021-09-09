using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Sms
{
    /// <summary>
    /// 短信基本配置
    /// </summary>
    public abstract class BaseSMS: IService
    {
        protected readonly IConfiguration Configuration;
        /// <summary>
        /// 最大发送次数
        /// </summary>
        public int MaxCount { get; set; }

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
