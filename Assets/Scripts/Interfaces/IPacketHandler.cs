namespace Interfaces
{
    public interface IPacketHandler
    {
        public void Read(byte[] packetReceived);
        public void Write();
        public void Handler(byte[] packetReceived);
    }
}