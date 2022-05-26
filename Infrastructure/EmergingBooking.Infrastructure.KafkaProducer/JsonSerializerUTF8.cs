using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.KafkaProducer
{
    internal class JsonSerializerUTF8<T> : ISerializer<T>
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly Encoding encoder;

        public JsonSerializerUTF8()
        {
            jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            encoder = Encoding.UTF8;
        }

        public byte[] Serialize(T data, SerializationContext context)
        {
            return encoder.GetBytes(JsonConvert.SerializeObject(data, jsonSerializerSettings));
        }
    }
}
