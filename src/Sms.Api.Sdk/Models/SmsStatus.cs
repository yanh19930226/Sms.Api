using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Sdk.Models
{
    public enum SmsStatus
    {
        失败 = -1,
        待处理 = 0,
        成功 = 1,
        处理中 = 2
    }
}
