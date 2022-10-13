using Confluent.Kafka;
using MessagePack;

namespace BookStore.BL.Serializers
{
    public class MsgPackSserializer <T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            var body = MessagePackSerializer.Serialize(data);
            return body;
        }
    }
}
