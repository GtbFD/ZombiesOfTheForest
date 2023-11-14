using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ConnectServer : MonoBehaviour
{
    public Text quantityPlayers;

    private string HOST;
    private int PORT;
    private int BUFFER_LENGTH = 1024;
    void Start()
    {
        HOST = "localhost";
        PORT = 11000;

        ConnectClientOnServer();
    }

    void ConnectClientOnServer() 
    {
        byte[] DataRecieved = new byte[BUFFER_LENGTH];

        IPEndPoint EndPointConnection = ConfigEndPoint();

        Socket SocketConnection = ConfigConnection(EndPointConnection);
        SocketConnection.Connect(EndPointConnection);

        Debug.Log("Socket connected to >" 
            + SocketConnection.RemoteEndPoint.ToString());

        /*byte[] msg = Encoding.ASCII.GetBytes("0");

        Player p = new Player();
        p.id = sender.GetHashCode();
        p.name = "Gutemberg";

        String msg2 = JsonConvert.SerializeObject(p);

        int bytesSent = sender.Send(Encoding.ASCII.GetBytes(msg2));

        int bytesRec = sender.Receive(bytes);

        string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

        quantityPlayers.text = data;

        if (data.IndexOf("<EOF>") > -1)
        {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
 
        quantityPlayers.text = connections + " - " + p.name; 

        /*sender.Shutdown(SocketShutdown.Both);
        sender.Close();*/
    }

    IPEndPoint ConfigEndPoint()
    {
        IPHostEntry IpHost = Dns.GetHostEntry(HOST);
        IPAddress IpAddress = IpHost.AddressList[0];
        IPEndPoint EndPointConnection = new IPEndPoint(IpAddress, PORT);

        return EndPointConnection;
    }

    Socket ConfigConnection(IPEndPoint EndPoint)
    {
        IPEndPoint EndPointConnection = EndPoint;

        Socket SocketConnection = new Socket(EndPointConnection.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        return SocketConnection;
    }

    void Update()
    {
        
    }
}
