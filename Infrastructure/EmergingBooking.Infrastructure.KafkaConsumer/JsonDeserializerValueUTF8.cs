using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace EmergingBooking.Infrastructure.KafkaConsumer
{
    internal class JsonDeserializerValueUTF8<TEntity> : IDeserializer<TEntity>
    {
        private readonly Encoding encoder;

        private readonly JsonCreationConverter<TEntity> CustomCreationConverter;

        public JsonDeserializerValueUTF8(JsonCreationConverter<TEntity> customCreationConverter)
        {
            encoder = Encoding.UTF8;
            CustomCreationConverter = customCreationConverter;
        }

        TEntity IDeserializer<TEntity>.Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonConvert.DeserializeObject<TEntity>(encoder.GetString(data.ToArray()));
        }
    }
}
