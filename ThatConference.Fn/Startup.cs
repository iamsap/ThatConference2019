using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThatConference.Fn;
using ThatConference.Fn.Config;
using ThatConference.Fn.Repositories;
using ThatConference.Fn.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ThatConference.Fn
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<AppConfig>()
                .Configure<IConfiguration>((settings, configuration) => { configuration.Bind("Config", settings); });
            builder.Services
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IOrderRepository, OrderRepository>();

        }
    }
}