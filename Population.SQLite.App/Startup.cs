using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Population.SQLite.App.Application;
using Population.SQLite.App.Infrastructure;
using Serilog;
using System; 

[assembly: FunctionsStartup(typeof(Population.SQLite.App.Api.Startup))]
namespace Population.SQLite.App.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddHttpContextAccessor();

            var local_root = Environment.CurrentDirectory;
            var azure_root = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";
            var actual_root = local_root ?? azure_root;

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .WriteTo.RollingFile(actual_root + "/log/{HalfHour}-log.txt")
              .CreateLogger();

            builder.Services.AddLogging(lb => { lb.AddSerilog(Log.Logger, true); });
        }
    }
}
