using System.Net.Sockets;
using Interfaces;
using packets.enums;
using server.utils;
using UnityEngine;

namespace handlers
{
    public class LoginPlayerHandler : IPacketHandler
    {

        private Socket connection;

        public LoginPlayerHandler(Socket connection)
        {
            this.connection = connection;
        }
        
        public void Handler(byte[] packetReceived)
        {
            Read(packetReceived);
        }
        
        public void Read(byte[] packetReceived)
        {
            var reader = new ReadPacket(packetReceived);
            var opcode = reader.ReadInt();

            if (opcode == (int)OpcodePackets.LOGIN_PLAYER_RESPONSE_SUCCESS)
            {
                var id = reader.ReadInt();
                PlayerData.id = id;
                Debug.Log($"Login success, ID RECEIVED {PlayerData.id}");
            }

            if (opcode == (int)OpcodePackets.LOGIN_PLAYER_RESPONSE_ERRO)
            {
                Debug.Log("Login error!");
            }
        }

        public void Write()
        {
            
        }

    }
}