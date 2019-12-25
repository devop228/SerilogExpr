using System;
using System.IO;
using Microsoft.Extensions.Configuration;
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
            var k = logSect.GetValue<string>("Serilog:WriteTo:1:Args:InstrumentationKey");
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(logSect)
                .CreateLogger();

            logger.Information("Hellow {name}!", "Yu");
            logger.Information("Hello {person}", new {FirstName="Yu", LastName="Zhu"});
            Console.ReadKey();
        }
    }
}
