using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.DeadLetterExchange
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var factory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                //Port =5672
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queueName = "Queue.SMS.Test";
                var exchangeName = "Exchange.SMS.Test";
                var key = "Route.SMS.Test";

                DeclareDelayQueue(channel, exchangeName, queueName, key);

                DeclareReallyConsumeQueue(channel, exchangeName, queueName, key);

                var body = Encoding.UTF8.GetBytes("info: test dely publish!");
                channel.BasicPublish(exchangeName + ".Delay", key, null, body);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 声明死信队列
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="exchangeName"></param>
        /// <param name="queueName"></param>
        /// <param name="key"></param>
        private static void DeclareDelayQueue(IModel channel, string exchangeName, string queueName, string key)
        {
            var retryDic = new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", exchangeName+".dl"},
                {"x-dead-letter-routing-key", key},
                {"x-message-ttl", 10000}
            };

            var ex = exchangeName + ".Delay";
            var qu = queueName + ".Delay";
            channel.ExchangeDeclare(ex, "topic");
            channel.QueueDeclare(qu, false, false, false, retryDic);
            channel.QueueBind(qu, ex, key);
        }
        /// <summary>
        /// 消费队列
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="exchangeName"></param>
        /// <param name="queueName"></param>
        /// <param name="key"></param>
        private static void DeclareReallyConsumeQueue(IModel channel, string exchangeName, string queueName, string key)
        {
            var ex = exchangeName + ".dl";
            channel.ExchangeDeclare(ex, "topic");
            channel.QueueDeclare(queueName, false, false, false);
            channel.QueueBind(queueName, ex, key);
        }
    }
}
