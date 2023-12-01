using System.Net;
using server.utils;

namespace server.config
{
    public class EndPointServer
    {

        public IPEndPoint GetEndPoint(string host, int port)
        {
            var serverInfo = new ServerInfo();
            
            var ipHost = Dns.GetHostEntry(host);
            var ipAddress = ipHost.AddressList[0];
            var endPointConnection = new IPEndPoint(ipAddress, port);

            return endPointConnection;
        }
    }
}