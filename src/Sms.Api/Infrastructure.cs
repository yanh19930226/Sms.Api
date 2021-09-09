using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api
{
    internal class InfrastructureConfig
    {
        public Infrastructure Infrastructure { get; set; }
    }

    public class Infrastructure
    {
        public string Mongodb { get; set; }
        public string RabbitMQ { get; set; }
    }
}
