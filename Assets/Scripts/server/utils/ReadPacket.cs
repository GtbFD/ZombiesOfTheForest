using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace server.utils
{
    public class ReadPacket
    {
        private byte[] readableBuffer;
        private List<byte> buffer;
        public int readPos;

    
        public ReadPacket(byte[] packet)
        {
            readableBuffer = packet.ToArray();
            readPos = 0;
            buffer = new List<byte>();
            buffer.AddRange(packet);
        }

    

        public int ReadByte()
        {
            if (readableBuffer.Length > readPos)
            {
                var _value = readableBuffer[readPos];
            
                readPos += 1; 
            
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte'!");
            }
        }
    
        public byte[] ReadBytes(int length)
        {
            if (readableBuffer.Length > readPos)
            {
                var _value = buffer.GetRange(readPos, length).ToArray();
            
                readPos += length; 
            
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte[]'!");
            }
        }

        public int ReadInt()
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = BitConverter.ToInt32(readableBuffer, readPos);
                readPos += 4;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'int'!");
            }
        }
    
        public long ReadLong()
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = BitConverter.ToInt64(readableBuffer, readPos);
                readPos += 8;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'long'!");
            }
        }
    
        public long ReadShort()
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = BitConverter.ToInt16(readableBuffer, readPos);
                readPos += 2;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'short'!");
            }
        }
    
        public float ReadFloat()
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = BitConverter.ToSingle(readableBuffer, readPos);
                readPos += 4;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'float'!");
            }
        }
    
        public double ReadDouble()
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = BitConverter.ToDouble(readableBuffer, readPos);
                readPos += 8;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'double'!");
            }
        }
    
        public bool ReadBool()
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = BitConverter.ToBoolean(readableBuffer, readPos);
                readPos += 1;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'bool'!");
            }
        }
    
        public string ReadString(int length)
        {
        
            if (readableBuffer.Length > readPos)
            {
                var value = Encoding.ASCII.GetString(readableBuffer, readPos, length);
                readPos += 1;
                return value;
            }else
            {
                throw new Exception("Could not read value of type 'string'!");
            }
        }
    }
}