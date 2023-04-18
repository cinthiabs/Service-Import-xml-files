using Business.Import.files.Interface;
using Business.Import.files.Service;
using Import_Files_Service;
using Infrastructure.Import.files.Interface;
using Infrastructure.Import.files.Query;

namespace Import_Files_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<IService, Service>();
                  //  services.AddTransient<IQuery, Query>();
                });
    }
}
