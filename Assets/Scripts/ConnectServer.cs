using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using handlers;
using Interfaces;
using Newtonsoft.Json;
using packets.enums;
using packets.request;
using server.config;
using server.utils;
using Unity.VisualScripting;
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
        var loginPlayerPacket = new LoginPlayerPacket()
        {
            opcode = (int)OpcodePackets.LOGIN_PLAYER,
            user = "admin",
            password = "admin"
        };

        var loginPlayerPacketSerialized =
            SerializePacket.Serialize(loginPlayerPacket);
        
        connection.Send(loginPlayerPacketSerialized);
        
        while (true)
        {
            var buffer = new ServerInfo().GetBuffer();
            var packetReceived = connection.Receive(buffer);
            var packetSerialized = Encoding.UTF8.GetString(buffer, 0, packetReceived);

            var packets = new List<IPacketHandler>()
            {
                new LoginPlayerHandler(connection),
                new DisconnectPlayerHandler(connection)
            };

            var packetHandler = new PacketManager(packets);
            packetHandler.Manager(packetSerialized);

        }
    }

    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        var disconnectPlayer = new DisconnectPlayerPacket()
        {
            opcode = (int)OpcodePackets.DISCONNECT_PLAYER
        };

        var disconnectPlayerSerializedPacket = SerializePacket.Serialize(disconnectPlayer);
        
        connection.Send(disconnectPlayerSerializedPacket);
    }
}
