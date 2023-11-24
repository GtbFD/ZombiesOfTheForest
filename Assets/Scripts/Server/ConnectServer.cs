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
using Unity.VisualScripting;
using Utils;

public class ConnectServer : MonoBehaviour
{

    //public string stateQuantityPlayers;
    public static Text quantityPlayers;

    private string HOST;
    private int PORT;
    private int BUFFER_LENGTH = 1024;

    Socket socketConnection;

    private PlayerMovement _playerMovement;

    void Start()
    {
        HOST = "localhost";
        PORT = 11000;

        ConnectClientOnServer();
        _playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void ConnectClientOnServer() 
    {
        
        IPEndPoint EndPointConnection = ConfigEndPoint();

        socketConnection = ConfigConnection(EndPointConnection);
        socketConnection.Connect(EndPointConnection);

        Debug.Log("Socket connected to >" 
            + socketConnection.RemoteEndPoint.ToString());

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
        var updateConnectedPlayers = new UpdateConnectedPlayers()
        {
            opcode = 1,
            quantity = 0
        };

        var packetToGetAllPlayerConnected =
            new SerializePacket().Serialize(updateConnectedPlayers);
        
        socketConnection.Send(packetToGetAllPlayerConnected);
        
        while (true)
        {
            var buffer = new byte[BUFFER_LENGTH];
            var bytesReceived = socketConnection.Receive(buffer);
            var packetReceived = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

            var packets = new List<IPacketHandler>()
            {
                new ConnectedPlayers(socketConnection),
                new DisconnectPlayerPacket(socketConnection)
            };

            var packetHandler = new PacketManager(packets);
            packetHandler.Manager(packetReceived);

            /*if (packetReceived.Contains("\"opcode\":0"))
            {
                //Debug.Log("Server authorization to disconnect");
                /*SocketConnection.Shutdown(SocketShutdown.Both);
                SocketConnection.Close();
            }
            
            if (packetReceived.Contains("\"opcode\":1"))
            {
                var updateConnectedPlayersPacket =
                    new DeserializePacket().Deserialize<UpdateConnectedPlayers>(packetReceived);

                Debug.Log("Quantity players received by server.");
                stateQuantityPlayers = "" + updateConnectedPlayersPacket.quantity;
            }

            if (packetReceived.Contains("\"opcode\":2"))
            {
                var playerPositionPacket = 
                    new DeserializePacket().Deserialize<PlayerPosition>(packetReceived);
                Debug.Log(playerPositionPacket);
            }*/

        }
    }

    void Update()
    {
        //quantityPlayers.text = stateQuantityPlayers + " player(s) connected";

        /*var playerPositionPacket = new SerializePacket().Serialize(_playerMovement.PlayerPosition());
        SocketConnection.Send(playerPositionPacket);*/
    }

    private void OnApplicationQuit()
    {
        var disconnectPlayer = new DisconnectPlayer()
        {
            opcode = 0
        };

        var disconnectPlayerSerializedPacket = new SerializePacket().Serialize(disconnectPlayer);
        
        socketConnection.Send(disconnectPlayerSerializedPacket);
    }
}
