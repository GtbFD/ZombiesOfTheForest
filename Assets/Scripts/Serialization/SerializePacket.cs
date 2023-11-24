using System.Text;
using Newtonsoft.Json;

namespace Serialization
{
    public class SerializePacket
    {
        public string ObjectToString<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    
        public byte[] Serialize<T>(T objectToSerialize)
        {
            var serializedObject = JsonConvert.SerializeObject(objectToSerialize);
            var serializedObjectPacket = Encoding.ASCII.GetBytes(serializedObject);
            return serializedObjectPacket;
        }
    }
}