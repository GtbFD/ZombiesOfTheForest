using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Threading;

public class ConnectServer : MonoBehaviour
{

    public String StateQuantityPlayers;
    public Text QuantityPlayers;

    private string HOST;
    private int PORT;
    private int BUFFER_LENGTH = 1024;

    Socket SocketConnection;
    void Start()
    {
        HOST = "localhost";
        PORT = 11000;

        ConnectClientOnServer();
    }

    void ConnectClientOnServer() 
    {
        
        IPEndPoint EndPointConnection = ConfigEndPoint();

        SocketConnection = ConfigConnection(EndPointConnection);
        SocketConnection.Connect(EndPointConnection);

        Debug.Log("Socket connected to >" 
            + SocketConnection.RemoteEndPoint.ToString());

        Thread ListenerPackets =
                new Thread(new ThreadStart(() => ListenPackets()));
        ListenerPackets.Start();
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

    void ListenPackets()
    {
        var commandToGetAllPlayerConnected = Encoding.ASCII.GetBytes("0001");
        SocketConnection.Send(commandToGetAllPlayerConnected);
        
        while (true)
        {
            byte[] DataRecieved = new byte[BUFFER_LENGTH];
            int BytesReciev = SocketConnection.Receive(DataRecieved);
            string CommandReciev = Encoding.ASCII.GetString(DataRecieved, 0, BytesReciev);
            
            if (CommandReciev.Contains("0001"))
            {
                Debug.Log("Quantity players received by server.");
                StateQuantityPlayers = CommandReciev.Substring(4);
            }

            if (CommandReciev.Contains("0000"))
            {
                Debug.Log("Server authorization to disconnect");
                SocketConnection.Shutdown(SocketShutdown.Both);
                SocketConnection.Close();
            }

        }
    }

    void Update()
    {
        QuantityPlayers.text = StateQuantityPlayers + " player(s) connected";
    }

    private void OnApplicationQuit()
    {
        var CommandToLeave = Encoding.ASCII.GetBytes("0000");

        SocketConnection.Send(CommandToLeave);
    }
}
