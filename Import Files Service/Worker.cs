using Business.Import.files.Interface;
using Business.Import.files.Service;
using Infrastructure.Import.files.Data;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Import_Files_Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IService _Service;
        public Worker(ILogger<Worker> logger, IService Service, IConfiguration Configuration)
        {
            _Service = Service;
            _logger = logger;
            Setting.ConnectionStringDefault = Configuration.GetConnectionString("DefaultConnection");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _Service.Execute();
                await Task.Delay(180000, stoppingToken);
            }
        }
    }
}