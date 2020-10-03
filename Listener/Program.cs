using slsr.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
//using Serilog;
using System;
using System.Threading.Tasks;
//using ILogger = Serilog.ILogger;

namespace slsr
{
    class Program
    {
        const int executionIntervalSeconds = 7;

        static async Task Main(string[] args)
        {

            var services = new ServiceCollection();
            ConfigureServices(services);

            //AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            //using ServiceProvider serviceProvider = services.BuildServiceProvider();
            //ILogger<Program> logger = serviceProvider.GetService<ILogger<Program>>();

            Scheduler scheduler = new Scheduler();
            JobKey vncCheckerJobKey = await scheduler.ScheduleJob<Checker>();
            await scheduler.ScheduleJob<ResponseChecker>(startAtTime: DateTime.Now.AddSeconds(executionIntervalSeconds / 2), //todo edit interval
                vncCheckerJobKey: vncCheckerJobKey);

            Console.WriteLine("Scheduled Jobs");
            //Console.ReadKey();
            CreateHostBuilder(args).Build().Run();
        }


        static void ConfigureServices(ServiceCollection services)
        {
            //services.AddTransient<MyApplication>()
            //        .AddScoped<IBusinessLayer, CBusinessLayer>()
            //        .AddSingleton<IDataAccessLayer, CDataAccessLayer>();

            //var serilogLogger = new LoggerConfiguration()
            //.WriteTo.File("slsr.txt")
            //.CreateLogger();

            //services.AddLogging(builder =>
            //{
            //    builder.SetMinimumLevel(LogLevel.Information);
            //    builder.AddSerilog(logger: serilogLogger, dispose: true);
            //});
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}