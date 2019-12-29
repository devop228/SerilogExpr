using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace SerilogExpr
{
    class Program
    {
        static void Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(typeof(Program).Assembly)
                .Build();

            var logSect = configRoot.GetSection("Logging");
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(logSect)
                .CreateLogger();

            var services = new ServiceCollection();
            services.AddSingleton<Main>();
            services.AddLogging(builder => {
                builder.AddSerilog(logger, dispose: true);
            });
            
            var provider = services.BuildServiceProvider();
            var main = provider.GetRequiredService<Main>();
            main.Run();
        }
    }
}
