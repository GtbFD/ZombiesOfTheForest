using Newtonsoft.Json;

namespace Serialization
{
    public class DeserializePacket
    {
        public T Deserialize<T>(string packet)
        {
            return JsonConvert.DeserializeObject<T>(packet);
        }
    }
}