using System.Net.Sockets;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using handlers;
using Interfaces;
using packets.enums;
using server.config;
using server.utils;
using UnityEngine.tvOS;
using UnityEngine.UI;
using utils.io;

public class ConnectServer : MonoBehaviour
{
    private Socket connectionTCP;
    private Socket connectionUDP;
    public Text UIConnectedPlayersText;

    private byte[] globalPacket;
    
    void Start()
    {
        ConnectOnServer();
    }

    void ConnectOnServer()
    {
        var serverInfo = new ServerInfo();
        
        /*
         * Connection test UDP
         */

        var udpClient = new UdpClient(serverInfo.GetHost(), serverInfo.GetPortUDP());
        
        
        /*
         * Test message send UDP
         */
        var writer = new WritePacket();
        writer.Write(1);
        writer.Write("GUGA");
        var packet = writer.BuildPacket();
        udpClient.Send(packet, packet.Length);
        
        var endPoint = new EndPointServer().GetEndPoint(serverInfo.GetHost(), serverInfo.GetPortTCP());
        
        connectionTCP = new Socket(endPoint.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
        connectionTCP = new ConfigClientConnection().ConfigTCP();
        connectionTCP.Connect(endPoint);

        Debug.Log("Connected to " + new ServerInfo().RemoteEndPoint(connectionTCP));
        
        

        var listenerPackets = new Thread((ListenPackets));
        listenerPackets.Start();
    }

    private void ListenPackets()
    {
        
        ConnectionPlayer.GetInstance().SetConnection(connectionTCP);
        
        var writer = new WritePacket();
        writer.Write((int)OpcodePackets.LOGIN_PLAYER);
        writer.Write("admin");
        writer.Write("admin");

        var packet = writer.BuildPacket();
        connectionTCP.Send(packet);
        
        while (true)
        {
            var buffer = new ServerInfo().GetBuffer();
            var packetReceived = connectionTCP.Receive(buffer);
            var packetSerialized = Encoding.ASCII.GetString(buffer, 0, packetReceived);
            var packetBytes = Encoding.ASCII.GetBytes(packetSerialized);
            if (packetBytes.Length != 0)
            {

                var packets = new List<IPacketHandler>()
                {
                    new LoginPlayerHandler(connectionTCP),
                    new DisconnectPlayerHandler(connectionTCP),
                };

                new PacketManager(packets).Manager(packetBytes);
            }
        }
    }

    void Update()
    {
        /*var reader = new ReadPacket(globalPacket);
        var opcode= reader.ReadInt();

        if (opcode == (int) OpcodePackets.UPDATE_CONNECTIONS_RESPONSE)
        {
            var quantity = reader.ReadInt();

            UIConnectedPlayersText.text = "" + quantity;
        }*/
    }

    private void OnApplicationQuit()
    {
        var writer = new WritePacket();
        writer.Write((int) OpcodePackets.DISCONNECT_PLAYER);

        var packet = writer.BuildPacket();

        connectionTCP.Send(packet);
    }
}
