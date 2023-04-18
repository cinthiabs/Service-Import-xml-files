using Business.Import.files.Interface;
using Business.Import.files.Service;

namespace Import_Files_Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IService _Service;
        public Worker(ILogger<Worker> logger, IService Service)
        {
            _Service = Service;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _Service.Execute();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}