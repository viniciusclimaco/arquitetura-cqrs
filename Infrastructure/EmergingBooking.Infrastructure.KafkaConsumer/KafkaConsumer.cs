using Confluent.Kafka;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.KafkaConsumer
{
    public class KafkaConsumer<TKeyType, TEventType> where TEventType : class
    {
        private readonly RetryPolicy<ConsumeResult<TKeyType, TEventType>> _kafkaRetryPolicy;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;
        private readonly JsonCreationConverter<TEventType> jsonCreationConverter;

        public KafkaConsumer(string group, string server, string topicName, JsonCreationConverter<TEventType> jsonCreationConverter)
        {
            _kafkaRetryPolicy = Policy.HandleResult<ConsumeResult<TKeyType, TEventType>>(r => r == null).WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) / 2));

            _topicName = topicName;
            _consumerConfig = new ConsumerConfig
            {
                GroupId = group,
                BootstrapServers = server,
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                EnablePartitionEof = true,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ApiVersionRequest = false
            };
        }

        public Func<TEventType, Task> OnConsumingAsync;

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            using (var consumer = new ConsumerBuilder<TKeyType, TEventType>(_consumerConfig)
                .SetKeyDeserializer(new JsonDeserializerKeyUTF8<TKeyType>())
                .SetValueDeserializer(new JsonDeserializerValueUTF8<TEventType>(jsonCreationConverter))
                .Build())
            {
                consumer.Subscribe(new[] { _topicName });
                try
                {

                    while (true)
                    {
                        try
                        {
                            var consumeResult = _kafkaRetryPolicy.Execute(() => consumer.Consume(cancellationToken));
                            if (consumeResult.IsPartitionEOF)
                            {
                                Console.WriteLine($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                                continue;
                            }

                            var entityJsonMessage = consumeResult.Value;
                            await OnConsumingAsync(entityJsonMessage);
                            Console.WriteLine($"Consumed message '{consumeResult.Value}' at: '{consumeResult.TopicPartitionOffset}'.");

                            try
                            {
                                consumer.Commit(consumeResult);
                            }
                            catch (KafkaException e)
                            {
                                Console.WriteLine($"Commit error: {e.Error.Reason}");
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            Console.WriteLine($"Ocurred error: {ex.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException exOpCan)
                {
                    consumer.Close();
                }
            }
        }
    }
}
