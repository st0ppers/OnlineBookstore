using BookStore.BL.Kafka.Settings;
using BookStore.BL.Serializers;
using BookStore.Cache;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Kafka
{
    public class ConsumerService : IHostedService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IOptionsMonitor<KafkaSettingsDelivery> _settingsDelivery;
        private readonly IOptionsMonitor<KafkaSettingsPurchase> _settingsPurchase;

        public ConsumerService(IBookRepo bookRepo, IOptionsMonitor<KafkaSettingsDelivery> settingsDelivery, IOptionsMonitor<KafkaSettingsPurchase> settingsPurchase)
        {
            _bookRepo = bookRepo;
            _settingsDelivery = settingsDelivery;
            _settingsPurchase = settingsPurchase;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var delivery = new ConsumerDelivery(_settingsDelivery, _bookRepo);
            var purchase = new ConsumerPurchase(_bookRepo, _settingsPurchase);
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    delivery.MyConsume();
                    purchase.MyConsume();
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Shutting down");
            return Task.CompletedTask;
        }
    }
}
