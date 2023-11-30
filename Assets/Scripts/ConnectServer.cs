using System.Net.Sockets;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using handlers;
using Interfaces;
using packets.enums;
using server.config;
using server.utils;
using utils.io;

public class ConnectServer : MonoBehaviour
{
    private Socket connection;
    
    void Start()
    {
        ConnectOnServer();
    }

    void ConnectOnServer()
    {
        var endPoint = new EndPointServer().GetEndPoint();
        
        connection = new Socket(endPoint.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
        connection = new ConfigClientConnection().Config();
        connection.Connect(endPoint);

        Debug.Log("Connected to " + new ServerInfo().RemoteEndPoint(connection));

        var listenerPackets = new Thread((ListenPackets));
        listenerPackets.Start();
    }

    private void ListenPackets()
    {
        
        ConnectionPlayer.GetInstance().SetConnection(connection);
        
        var writer = new WritePacket();
        writer.Write((int)OpcodePackets.LOGIN_PLAYER);
        writer.Write("admin");
        writer.Write("admin");

        var packet = writer.BuildPacket();
        connection.Send(packet);
        
        while (true)
        {
            var buffer = new ServerInfo().GetBuffer();
            var packetReceived = connection.Receive(buffer);
            var packetSerialized = Encoding.ASCII.GetString(buffer, 0, packetReceived);

            var packets = new List<IPacketHandler>()
            {
                new LoginPlayerHandler(connection),
                new DisconnectPlayerHandler(connection),
                new PlayerLocalizationHandler(connection)
            };

            var packetHandler = new PacketManager(packets);
            packetHandler.Manager(Encoding.ASCII.GetBytes(packetSerialized));

        }
    }

    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        var writer = new WritePacket();
        writer.Write((int) OpcodePackets.DISCONNECT_PLAYER);

        var packet = writer.BuildPacket();

        connection.Send(packet);
    }
}
