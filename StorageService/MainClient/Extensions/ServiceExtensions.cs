using MassTransit;
using GreenPipes;
using CrossCuttingLayer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
using MainClient.Interfaces;
using MainClient.Services;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using AzureUploader.Services;
using MainClient.Hubs;

namespace MainClient.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>services.AddCors(options => 
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>services.Configure<IISOptions>(options => {
            
        });
        public static void ConfigureIISServerOptions(this IServiceCollection services) => services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
            options.MaxRequestBodySize = int.MaxValue;
        });
        public static void ConfigureKestrelServerOptions(this IServiceCollection services) => services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
            options.Limits.MaxRequestBodySize = int.MaxValue;
        });
        public static void ConfigureFormOptions(this IServiceCollection services) => services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
            options.MultipartHeadersLengthLimit = int.MaxValue;
        });
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigurFileStorageService(this IServiceCollection services, IConfiguration configuration) => services.AddSingleton<IFileStorage>((sp) =>
        {
            var loggerFactory = sp.GetService<ILoggerManager>();
            return new BlobStorage(configuration.GetConnectionString("Default"), "root", loggerFactory);
        });
        public static void ConfigurBlockUploaderService(this IServiceCollection services, IConfiguration configuration) => services.AddSingleton((sp) =>
        {
            return new BlockBlobUploader(configuration.GetConnectionString("Default"), "root");
        }); 
        public static void ConfigurBlobAccessService(this IServiceCollection services, IConfiguration configuration) => services.AddScoped((sp) => new BlobAccess(configuration.GetConnectionString("Default")));
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                    {
                        h.Username(RabbitMqConsts.UserName);
                        h.Password(RabbitMqConsts.Password);
                    });
                    cfg.ReceiveEndpoint("NotifyClient", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<NotificationConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }

    }
}
