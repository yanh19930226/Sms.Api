using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Model
{
    public class SmsEnums
    {
        /// <summary>
        /// 短信运营商
        /// </summary>
        public enum SmsType
        {
            Test1Sms=1,
            Test2Sms=2,
            Test3Sms=3
        }
        /// <summary>
        /// 发送状态
        /// </summary>
        public enum MarketingSmsStatus
        {
            待发送,
            已发送,
            发送失败
        }
        public enum SmsStatus
        {
            失败 = -1,
            待处理 = 0,
            成功 = 1
        }
    }
}
