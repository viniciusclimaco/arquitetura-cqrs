using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EmergingBooking.Infrastructure.KafkaConsumer
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object? ReadJson(JsonReader reader, 
                                         Type objectType, 
                                         object? existingValue, 
                                         JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            T target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target; 
        }
    }
}
