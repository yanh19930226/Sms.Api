using PeterKottas.DotNetCore.WindowsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Bus
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceRunner<MainService>.Run(config =>
            {
                config.SetServiceInfo();

                config.Service(serviceConfig =>
                {
                    serviceConfig.UseAutofac();
                    serviceConfig.UseServiceFactory();

                    serviceConfig.OnStart((service, extraParams) =>
                    {
                        service.Start();
                    });

                    serviceConfig.OnStop(service =>
                    {
                        service.Stop();
                    });

                    serviceConfig.OnError(Console.WriteLine);
                });
            });
        }
    }
}
