using System.Net.Sockets;

namespace server.config
{
    public class ConfigClientConnection
    {
        
        public Socket Config()
        {
            var endPointConnection = new EndPointServer().GetEndPoint();

            var socketConnection = new Socket(endPointConnection.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            return socketConnection;
        }
    }
}