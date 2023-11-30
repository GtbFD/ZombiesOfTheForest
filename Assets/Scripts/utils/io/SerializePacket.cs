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
    
        public static byte[] Serialize(string packetToSerialize)
        {
            /*var serializedObject = JsonConvert.SerializeObject(objectToSerialize);
            serializedObject += "<EOF>";*/
            var serializedObjectPacket = Encoding.ASCII.GetBytes(packetToSerialize);
            return serializedObjectPacket;
        }
    }
}