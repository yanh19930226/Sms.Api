﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api
{
    public class TokenModel
    {
        public string Token { get; set; }//token内容
        public long Expires { get; set; }//过期时间
    }
}
