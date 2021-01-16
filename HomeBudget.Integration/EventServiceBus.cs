using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace HomeBudget.Integration
{
    public class EventServiceBus: IEventBus
    {
        const string BROKER_NAME = "HomeBudget_event_bus";

        private readonly ILogger<EventServiceBus> _logger;
        private readonly int _retries;
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _queueName;
        
        public EventServiceBus(ILogger<EventServiceBus> logger, string _queueName)
        {
            _logger = logger;
            _retries = 3;
            _connectionFactory = new ConnectionFactory() {HostName = "localhost"};
        }

        public void Publish(IIntegrationEvent @event)
        {
            var retryPolicy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetryAsync(_retries, (attempt) => TimeSpan.FromSeconds(attempt), (ex, time) =>
                {
                    _logger.LogError(ex, $"Cannot publish the event, broker unreachable, eventId: {@event.Id}");
                });
            
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(BROKER_NAME, "direct");

                    var msg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
                    
                    var props = channel.CreateBasicProperties();
                    props.DeliveryMode = 2;

                    _logger.LogTrace($"Publishing event: {@event.Id}");

                    retryPolicy.ExecuteAsync(async () =>
                    {
                        channel.BasicPublish(
                            BROKER_NAME
                            , @event.GetType().Name,
                            true,
                            props,
                            msg
                        );
                    });
                    
                }
            }
        }

        public void Subscribe<T, TH>() where T : IIntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            _logger.LogInformation($"Starting consume {nameof(T)}");

            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueBind(queue: _queueName,
                    exchange: "logs",
                    routingKey: "");


                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);

                channel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer
                );
                
                _logger.LogInformation($"Consume started {nameof(T)}");
            }
        }

        public void Unsubscribe<T, TH>() where T : IIntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new System.NotImplementedException();
        }
    }
}