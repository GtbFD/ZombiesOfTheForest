using System.Net.Sockets;
using Interfaces;
using packets.enums;
using UnityEngine;
using utils.io;

namespace handlers
{
    public class DisconnectPlayerHandler : IPacketHandler
    {

        private Socket socketConnection;

        public DisconnectPlayerHandler(Socket socketConnection)
        {
            this.socketConnection = socketConnection;
        }
        
        public void Handler(string packetReceived)
        {
            Read(packetReceived);
        }
        
        public void Read(string packetReceived)
        {
            var opcode = PacketIdentifier.Opcode(packetReceived);
            if (opcode == (int)OpcodePackets.DISCONNECT_PLAYER_RESPONSE)
            {
                Write();
            }
        }

        public void Write()
        {
            socketConnection.Shutdown(SocketShutdown.Both);
            socketConnection.Close();
        }

        
    }
}