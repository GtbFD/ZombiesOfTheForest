using System;
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
using UnityEngine.UI;
using utils.io;
using Random = System.Random;

public sealed class ConnectServer : MonoBehaviour
{
    private Socket connectionTCP;
    private Socket connectionUDP;
    public Text UIConnectedPlayersText;

    public static byte[] globalPacket;

    void Start()
    {
        ConnectOnServer();
    }

    void ConnectOnServer()
    {
        var serverInfo = new ServerInfo();
        

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
                globalPacket = packetBytes;

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
        var readerPacket = new ReadPacket(globalPacket);
        var opcode = readerPacket.ReadInt();

        if (opcode == (int)OpcodePackets.UPDATE_CONNECTIONS_RESPONSE)
        {
            UIConnectedPlayersText.text = " " + readerPacket.ReadInt();
        }

        if (opcode == (int)OpcodePackets.PLAYER_LOCALIZATION_RESPONSE)
        {
            Debug.Log($"x {readerPacket.ReadFloat()}, y {readerPacket.ReadFloat()}, z {readerPacket.ReadFloat()}");
        }
    }

    private void OnApplicationQuit()
    {
        var writer = new WritePacket();
        writer.Write((int) OpcodePackets.DISCONNECT_PLAYER);

        var packet = writer.BuildPacket();

        connectionTCP.Send(packet);
    }
}
