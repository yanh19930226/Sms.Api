using EasyNetQ;
using EasyNetQ.Scheduling;
using Microsoft.Extensions.Configuration;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using Sikiro.Nosql.Mongo;
using Sms.Api.Model;
using Sms.Api.Service.Sms;
using Sms.Api.ToolKits;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sms.Api.Bus
{
    public class MainService : IMicroService
    {
        private readonly IBus _bus;
        private readonly SmsFactory _smsFactory;
        private readonly IConfiguration _configuration;
        private readonly MongoRepository _mongoProxy;

        public MainService(IBus bus, MongoRepository mongoProxy, SmsFactory smsFactory, IConfiguration configuration)
        {
            _bus = bus;
            _mongoProxy = mongoProxy;
            _smsFactory = smsFactory;
            _configuration = configuration;
        }
        public void Send(SmsModel item)
        {
            var isSuccess = _smsFactory.Create(item.Type).SendSMS(item.Mobiles, item.Content, _configuration["Sms:SignName"]);
            if (isSuccess)
                Success(item);
            else
                Fail(item);
        }
        private void ReSend(SmsModel smsModel)
        {
            var model = smsModel.MapTo<SmsModel, SmsQueueModel>();
            model.SendCount++;

            _bus.FuturePublish(TimeSpan.FromSeconds(30 * model.SendCount), model, SmsQueueModelKey.Topic);
        }
        private void Success(SmsModel model)
        {
            model.Status = SmsEnums.SmsStatus.成功;
            model.CreateDateTime = DateTime.Now;
            _mongoProxy.Add(MongoKey.SmsDataBase, MongoKey.SmsCollection + "_" + DateTime.Now.ToString("yyyyMM"), model);
        }

        private void Fail(SmsModel model)
        {
            model.Status = SmsEnums.SmsStatus.失败;
            model.CreateDateTime = DateTime.Now;
            _mongoProxy.Add(MongoKey.SmsDataBase, MongoKey.SmsCollection + "_" + DateTime.Now.ToString("yyyyMM"), model);
        }
        public void Start()
        {
            Console.WriteLine("I started");

            _bus.Subscribe<SmsQueueModel>("", msg =>
            {
                var sms = msg.MapTo<SmsQueueModel, SmsModel>();

                try
                {
                   Send(sms);
                }
                catch (WebException e)
                {
                    e.WriteToFile();
                    ReSend(sms);
                }
                catch (Exception e)
                {
                    e.WriteToFile();
                }
            }, a =>
            {
                a.WithTopic(SmsQueueModelKey.Topic);
            });
        }
        
        public void Stop()
        {
            ConfigServer.Container?.Dispose();
            Console.WriteLine("I stopped");
        }
    }
}
