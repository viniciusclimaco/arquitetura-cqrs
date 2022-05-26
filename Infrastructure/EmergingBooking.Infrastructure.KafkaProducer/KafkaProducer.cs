using Confluent.Kafka;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.KafkaProducer
{
    internal class KafkaProducer<TKeyType, TEntity> : IDisposable
    {
        private readonly AsyncRetryPolicy<DeliveryResult<TKeyType, TEntity>> _kafkaRetryPolicy;
        private readonly string _topicName;
        private readonly ProducerConfig _producerConfig;

        public KafkaProducer(string topicName, string server)
        {
            _kafkaRetryPolicy = Policy.HandleResult<DeliveryResult<TKeyType, TEntity>>(r => r == null).WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) / 2));
            _topicName = topicName;
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = server
            };
        }

        public async Task ProduceMessage(TEntity entity, TKeyType partitionKey)
        {
            using (var producer = new ProducerBuilder<TKeyType, TEntity>(_producerConfig)
                .SetKeySerializer(new JsonSerializerUTF8<TKeyType>())
                .SetValueSerializer(new JsonSerializerUTF8<TEntity>())
                .Build())                
            {
                var message = new Message<TKeyType, TEntity>
                {
                    Key = partitionKey,
                    Value = entity
                };
                await _kafkaRetryPolicy.ExecuteAsync(() => producer.ProduceAsync(_topicName, message));
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
