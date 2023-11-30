using System;
using System.Collections.Generic;
using System.Text;

namespace server.utils
{
    public class WritePacket
    {
        private List<byte> buffer;
    
        public WritePacket()
        {
            buffer = new List<byte>();
        }
    
        public void Write(byte[] value)
        {
            buffer.AddRange(value);
        }

        public void Write(short value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
        }
    
        public void Write(int value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
        }
    
        public void Write(float value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
        }
    
        public void Write(double value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
        }
    
        public void Write(bool value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
        }
    
        public void Write(long value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
        }
    
        public void Write(string value)
        {
            buffer.AddRange(Encoding.ASCII.GetBytes(value));
        }
    
        public byte[] BuildPacket()
        {
            return buffer.ToArray();
        }
    
        public void ClearBuffer()
        {
            buffer.Clear();
        }
    }
}