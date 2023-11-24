using System.Text;
using Server;

namespace Utils
{
    public class PacketEncoding
    {
        public string String(int packet)
        {
            var buffer = new ServerInfo().GetBuffer();
            
            return Encoding.UTF8.GetString(buffer, 0, packet);
        }
    }
}