using System;
using System.Net.Sockets;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Packets
{
    public class ConnectedPlayers : IPacketHandler
    {
        
        private Socket socketConnection;

        public ConnectedPlayers(Socket socketConnection)
        {
            this.socketConnection = socketConnection;
        }
        
        public void Read(string packetReceived)
        {
            
        }

        public void Write()
        {
            
        }

        public void Handler(string packetReceived)
        {
            if (PacketIdentifier.Opcode(packetReceived) == 1)
            {
                
            }
        }
    }
}