using System;
using System.Collections.Generic;
using Interfaces;
using server.utils;
using UnityEngine;

namespace utils.io
{
    public class PacketManager
    {
        private List<IPacketHandler> packets;

        public PacketManager(List<IPacketHandler> packets)
        {
            this.packets = packets;
        }

        public void Manager(byte[] packetReceived)
        {
            var reader = new ReadPacket(packetReceived);
            Debug.Log("[@] <- PACKET_RECEIVED - ID: " + reader.ReadInt());
            foreach (var packet in packets)
            {
                packet.Handler(packetReceived);
            }
        }
    }
}