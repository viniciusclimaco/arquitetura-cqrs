using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace EmergingBooking.Infrastructure.KafkaConsumer
{
    internal class JsonDeserializerKeyUTF8<TKeyType> : IDeserializer<TKeyType>
    {
        private readonly Encoding encoder;
        public JsonDeserializerKeyUTF8()
        {
            encoder = Encoding.UTF8;
        }

        public TKeyType Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonConvert.DeserializeObject<TKeyType>(encoder.GetString(data.ToArray()));
        }
    }
}
