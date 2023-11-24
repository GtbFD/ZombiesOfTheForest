namespace Interfaces
{
    public interface IPacketHandler
    {
        public void Read(string packetReceived);
        public void Write();
        public void Handler(string packetReceived);
    }
}