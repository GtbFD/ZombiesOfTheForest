using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Utils
{
    public static class PacketIdentifier
    {
        public static int Opcode(string packetReceived)
        {
            //Debug.Log(packetReceived);
            var pattern = @".*""opcode"":(.*?[0-9]*)";
            var matchResult = Regex.Match(packetReceived, @pattern).Groups[1].Value;
            var opcode = int.Parse(matchResult);

            return opcode;
        }
    }
}