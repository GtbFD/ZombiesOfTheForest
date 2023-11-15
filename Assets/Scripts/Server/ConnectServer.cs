using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

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

        byte[] CommandToLeave = Encoding.ASCII.GetBytes("<EOF>");

        SocketConnection.Send(CommandToLeave);

        int BytesReciev = SocketConnection.Receive(DataRecieved);
        string CommandReciev = Encoding.ASCII.GetString(DataRecieved, 0, BytesReciev);


        if (CommandReciev.IndexOf("<EOF>") > -1)
        {
            Debug.Log("Server authorization to disconnect");
            SocketConnection.Shutdown(SocketShutdown.Both);
            SocketConnection.Close();
        }
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
