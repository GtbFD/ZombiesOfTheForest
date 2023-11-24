using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using Classes;
using Interfaces;
using Newtonsoft.Json;
using Packets;
using Serialization;
using Server;
using Unity.VisualScripting;
using Utils;

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
        /*var updateConnectedPlayers = new UpdateConnectedPlayers()
        {
            opcode = 1,
            quantity = 0
        };

        var packetToGetAllPlayerConnected =
            new SerializePacket().Serialize(updateConnectedPlayers);
        
        connection.Send(packetToGetAllPlayerConnected);*/
        
        while (true)
        {
            var buffer = new ServerInfo().GetBuffer();
            var packetReceived = connection.Receive(buffer);
            var packetSerialized = new PacketEncoding().String(packetReceived);

            var packets = new List<IPacketHandler>()
            {
                //new ConnectedPlayers(connection),
                new DisconnectPlayerPacket(connection)
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
        var disconnectPlayer = new DisconnectPlayer()
        {
            opcode = 0
        };

        var disconnectPlayerSerializedPacket = new SerializePacket().Serialize(disconnectPlayer);
        
        connection.Send(disconnectPlayerSerializedPacket);
    }
}
