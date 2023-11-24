using System.Net.Sockets;
using Interfaces;
using UnityEngine;
using Utils;

namespace Packets
{
    public class DisconnectPlayerPacket : IPacketHandler
    {

        private Socket socketConnection;

        public DisconnectPlayerPacket(Socket socketConnection)
        {
            this.socketConnection = socketConnection;
        }
        
        public void Read(string packetReceived)
        {
            
        }

        public void Write()
        {
            socketConnection.Shutdown(SocketShutdown.Both);
            socketConnection.Close();
        }

        public void Handler(string packetReceived)
        {
            Debug.Log(PacketIdentifier.Opcode(packetReceived));
            if (PacketIdentifier.Opcode(packetReceived) == 0)
            {
                Debug.Log("Packet to disconnect list player");
                Read(packetReceived);
                Write();
            }
        }
    }
}