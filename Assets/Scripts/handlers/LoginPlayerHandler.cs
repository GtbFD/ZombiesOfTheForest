using System.Net.Sockets;
using Interfaces;
using packets.enums;
using packets.response;
using UnityEngine;
using utils.io;

namespace handlers
{
    public class LoginPlayerHandler : IPacketHandler
    {

        private Socket connection;

        public LoginPlayerHandler(Socket connection)
        {
            this.connection = connection;
        }
        
        public void Handler(string packetReceived)
        {
            Read(packetReceived);
        }
        
        public void Read(string packetReceived)
        {
            var opcode = PacketIdentifier.Opcode(packetReceived);

            if (opcode == (int)OpcodePackets.LOGIN_PLAYER_RESPONSE_SUCCESS)
            {
                var loginPlayerPacket = DeserializePacket.Deserialize<LoginPlayerResponsePacket>(packetReceived);
                Debug.Log("Login success!");
            }

            if (opcode == (int)OpcodePackets.LOGIN_PLAYER_RESPONSE_ERRO)
            {
                Debug.Log("Login error!");
            }
        }

        public void Write()
        {
            throw new System.NotImplementedException();
        }

    }
}