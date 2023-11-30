using System.Net.Sockets;

namespace server.utils
{
    public sealed class ConnectionPlayer
    {
        private static ConnectionPlayer instance;
        private Socket connection;

        public static ConnectionPlayer GetInstance()
        {
            if (instance == null)
            {
                instance = new ConnectionPlayer();
            }

            return instance;
        }

        public void SetConnection(Socket connection)
        {
            this.connection = connection;
        }

        public Socket GetConnection()
        {
            return connection;
        }
    }
}