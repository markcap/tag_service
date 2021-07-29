using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace TagService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Catch and log errors that occur during start-up.
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            // Process all log events thru the Serilog pipeline. Configuration via appsettings.json
            .UseSerilog((hosting, logerconfig) => logerconfig.ReadFrom.Configuration(hosting.Configuration))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://+:5000").UseStartup<Startup>();
            });
    }
}
