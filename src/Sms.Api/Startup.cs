using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sikiro.Nosql.Mongo;
using EasyNetQ.Scheduling;
using NSwag.AspNetCore;
using System.Reflection;
using NJsonSchema;
using System.Linq;

namespace Sms.Api
{
    public class Startup
    {
        private readonly InfrastructureConfig _infrastructureConfig;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _infrastructureConfig = configuration.Get<InfrastructureConfig>();
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
               
            });
            services.RegisterEasyNetQ(_infrastructureConfig.Infrastructure.RabbitMQ, a =>
            {
                a.EnableDeadLetterExchangeAndMessageTtlScheduler();
            });
            services.AddSingleton(new MongoRepository(_infrastructureConfig.Infrastructure.Mongodb));
            services.AddService();
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Sms.Api";
                    document.Info.Description = "yande Sms.Api";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "yande",
                        Email = "891367701@qq.com",
                        Url = "http://yande.buzz"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "yande",
                        Url = "http://yande.buzz"
                    };
                };
            });
            //services.AddOpenApiDocument(settings =>
            //{
            //    settings.AddSecurity("身份认证Token", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme()
            //    {
            //        Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
            //        Name = "Authorization",
            //        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            //        Type = NSwag.OpenApiSecuritySchemeType.ApiKey
            //    });
            //});
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi(); //添加swagger生成api文档（默认路由文档 /swagger/v1/swagger.json）
            app.UseSwaggerUi3();//添加Swagger UI到请求管道中(默认路由: /swagger).
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
