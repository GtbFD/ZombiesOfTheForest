using System.Text;
using Newtonsoft.Json;

namespace utils.io
{
    public class SerializePacket
    {
        public static string ObjectToString<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    
        public static byte[] Serialize<T>(T objectToSerialize)
        {
            var serializedObject = JsonConvert.SerializeObject(objectToSerialize);
            var serializedObjectPacket = Encoding.ASCII.GetBytes(serializedObject);
            return serializedObjectPacket;
        }
    }
}