namespace server.utils
{
    public class PlayerData
    {
        public static int id { get; set; }
        public static float x { get; set; }
        public static float y { get; set; }
        public static float z { get; set; }
        
        private static PlayerData playerData;

        public static PlayerData GetInstance()
        {
            if (playerData == null)
            {
                playerData = new PlayerData();
            }

            return playerData;
        }
    }
}