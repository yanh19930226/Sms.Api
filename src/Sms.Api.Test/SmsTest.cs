using Sms.Api.Sdk;
using Sms.Api.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sms.Api.Test
{
    
    public class SmsTests
    {
        public SmsTests()
        {
            SmsClient.Init("http://localhost:5000/api", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoieWFuZGUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6InlhbmRlIiwibmJmIjoxNjMxMjY5NTQxLCJleHAiOjE2MzM4NjE1NDEsImlzcyI6Imd1ZXRTZXJ2ZXIiLCJhdWQiOiJndWV0Q2xpZW50In0.hU0whP9Fidah0NCq1x-IxXP2_q7EnDY3t3X7qM0ulcY");
        }

        [Fact]
        public void Sms_Get_IsTrue()
        {
            var result = SmsClient.Get("9b096de3ad394d1abc81c805145cadaf");
            Assert.Equal(result.IsSuccess, true);
        }

        [Fact]
        public void Sms_Search_IsTrue()
        {
            var result = SmsClient.Search(new SearchEntity
            {
                Mobile = "18988111111"
            });

            Assert.Equal(result.IsSuccess, true);
        }

        [Fact]
        public void Sms_Send_IsTrue()
        {
            var result = SmsClient.Send(new SendEntity
            {
                Content = "yande",
                Mobiles = new List<string>
                {
                    "18988111111",
                    "18988111110",
                    "18988111112",
                    "18988111113"
                },
                Type = SmsType.Test2Sms
            });

            Assert.Equal(result.IsSuccess, true);
        }

        [Fact]
        public void Sms_SendAsync_IsTrue()
        {
            var result = SmsClient.SendAsync(new SendEntity
            {
                Content = "yande",
                Mobiles = new List<string>
                {
                    "18988111111",
                    "18988111110",
                    "18988111112",
                    "18988111113"
                },
                Type = SmsType.Test1Sms
            }).Result;

            Assert.Equal(result.IsSuccess, true);
        }

        [Fact]
        public void Sms_BatchSendAsync_IsTrue()
        {
            var result = SmsClient.SendAsync(new List<SendEntity>
            {
                new SendEntity
                {
                    Content = "yande",
                    Mobiles = new List<string>
                    {
                        "18988111111",
                        "18988111110",
                        "18988111112",
                        "18988111113"
                    },
                    Type = SmsType.Test2Sms,
                    TimeSendDateTime =  DateTime.Now.AddMinutes(1)
                },
                new SendEntity
                {
                    Content = "yande",
                    Mobiles = new List<string>
                    {
                        "18988111111",
                        "18988111110",
                        "18988111112",
                        "18988111113"
                    },
                    Type = SmsType.Test1Sms,
                    TimeSendDateTime =  DateTime.Now.AddMinutes(1)
                },
                new SendEntity
                {
                    Content = "yande",
                    Mobiles = new List<string>
                    {
                        "18988111111",
                        "18988111110",
                        "18988111112",
                        "18988111113"
                    },
                    Type = SmsType.Test1Sms,
                    TimeSendDateTime = DateTime.Now.AddMinutes(1)
                }
            }).Result;

            Assert.Equal(result.IsSuccess, true);
        }
    }
}
