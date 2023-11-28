namespace SuperPlay.GameServer.Domain.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string DeviceId { get; set; }

        public int Coins { get; set; }

        public int Rolls { get; set; }
    }
}