using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service.Base
{
    public class SmsConfig
    {
        public Sms Sms { get; set; }
    }

    public class Sms
    {
        public TestSms1Config TestSms1Config { get; set; }

        public TestSms2Config TestSms2Config { get; set; }

        public TestSms3Config TestSms3Config { get; set; }
    }

    public class TestSms1Config
    {
        public int MaxCount { get; set; }
        public int Age { get; set; }

        public string Name { get; set; }
    }

    public class TestSms2Config
    {
        public int MaxCount { get; set; }
        public int Age { get; set; }

        public string Name { get; set; }
    }

    public class TestSms3Config
    {
        public int MaxCount { get; set; }
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
