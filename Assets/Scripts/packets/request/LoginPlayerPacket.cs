namespace packets.request
{
    public class LoginPlayerPacket
    {
        public int opcode { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}