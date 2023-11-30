using System;
using System.Net.Sockets;
using System.Threading;
using Interfaces;
using packets.enums;
using server.utils;
using UnityEngine;
using utils.io;

namespace handlers
{
    public class PlayerLocalizationHandler : IPacketHandler
    {
        private Socket socketConnection;

        public PlayerLocalizationHandler(Socket socketConnection)
        {
            this.socketConnection = socketConnection;
        }
        
        public void Handler(byte[] packetReceived)
        {
            Read(packetReceived);
        }
        
        public void Read(byte[] packetReceived)
        {
            var reader = new ReadPacket(packetReceived);
            var opcode = reader.ReadInt();
            if (opcode == (int)OpcodePackets.PLAYER_LOCALIZATION_RESPONSE)
            {
                //Debug.Log("POSITION");
                var x = reader.ReadFloat();
                var y = reader.ReadFloat();
                var z = reader.ReadFloat();
                
                Debug.Log("x: " + x + ", y: " + y + ", z: " + z);
            }
        }

        public void Write()
        {
            
        }
    }
}