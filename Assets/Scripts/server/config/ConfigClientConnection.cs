using System.Net.Sockets;
using server.utils;

namespace server.config
{
    public class ConfigClientConnection
    {
        
        public Socket ConfigTCP()
        {
            var serverInfo = new ServerInfo();
            
            var endPointConnection = new EndPointServer().GetEndPoint(serverInfo.GetHost(), serverInfo.GetPortTCP());

            var socketConnection = new Socket(endPointConnection.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            return socketConnection;
        }
        
        public Socket ConfigUDP()
        {
            var serverInfo = new ServerInfo();
            
            var endPointConnection = new EndPointServer().GetEndPoint(serverInfo.GetHost(), serverInfo.GetPortUDP());

            var socketConnection = new Socket(endPointConnection.AddressFamily,
                SocketType.Dgram, ProtocolType.Udp);

            return socketConnection;
        }
    }
}