using System.Net;

namespace Server
{
    public class EndPointServer
    {

        public IPEndPoint GetEndPoint()
        {
            var serverInfo = new ServerInfo();
            
            var ipHost = Dns.GetHostEntry(serverInfo.GetHost());
            var ipAddress = ipHost.AddressList[0];
            var endPointConnection = new IPEndPoint(ipAddress, serverInfo.GetPort());

            return endPointConnection;
        }
    }
}