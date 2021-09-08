using Sms.Api.Model;
using Sms.Api.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Sms
{
    public class SmsFactory
    {
        private readonly Test1Sms _test1Sms;
        private readonly Test2Sms _test2Sms;
        private readonly Test3Sms _test3Sms;
        public SmsFactory(Test1Sms test1Sms, Test2Sms test2Sms, Test3Sms test3Sms)
        {
            _test1Sms = test1Sms;
            _test2Sms = test2Sms;
            _test3Sms = test3Sms;
        }

        public BaseSMS Create(SmsEnums.SmsType type)
        {
            switch (type)
            {
                case SmsEnums.SmsType.Test1Sms: return _test1Sms;
                case SmsEnums.SmsType.Test2Sms: return _test2Sms;
                case SmsEnums.SmsType.Test3Sms: return _test3Sms;
                default: throw new SmsException("无法识别的type");
            }
        }
    }
}
