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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System;

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

            #region Authentication

            services.AddAuthentication(x =>
               {
                   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }).AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,//�Ƿ���֤Issuer
                       ValidateAudience = true,//�Ƿ���֤Audience
                       ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                       ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                       ValidAudience = Configuration["Audience"],//Audience
                       ValidIssuer = Configuration["Issuer"],//Issuer���������ǩ��jwt������һ��
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//SecurityKey
                   };
               });

            #endregion

            services.AddOpenApiDocument(settings =>
            {
                settings.PostProcess = document =>
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
                settings.AddSecurity("�����֤Token", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme()
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey
                });
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi(); //���swagger����api�ĵ���Ĭ��·���ĵ� /swagger/v1/swagger.json��
            app.UseSwaggerUi3();//���Swagger UI������ܵ���(Ĭ��·��: /swagger).
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
