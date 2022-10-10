using System.ComponentModel;
using System.Timers;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Threading.Timer;


namespace BookStore.BL.Background
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private readonly Timer _timer;
        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
            _timer = new Timer(DoWork, null, 0, 2000);
        }

        private void DoWork(object? state)
        {
            Thread.Sleep(2000);
            _logger.LogInformation($"Hello from {nameof(MyBackgroundService)} {DateTime.Now}");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Hello from {nameof(MyBackgroundService)}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Bye from {nameof(MyBackgroundService)}");
            return Task.CompletedTask;
        }

    
    }
}
