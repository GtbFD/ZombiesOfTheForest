﻿using System.Net.Sockets;
using Interfaces;
using packets.enums;
using server.utils;
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
        
        public void Handler(byte[] packetReceived)
        {
            Read(packetReceived);
        }
        
        public void Read(byte[] packetReceived)
        {
            var reader = new ReadPacket(packetReceived);
            var opcode = reader.ReadInt();

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