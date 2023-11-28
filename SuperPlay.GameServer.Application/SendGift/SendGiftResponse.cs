using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.SendGift
{
	public sealed class SendGiftResponse
	{
        public bool IsSuccess { get; private set; }

        public string ErrorMessage { get; private set; }

        public Player Player { get; private set; }

        public static SendGiftResponse Success(Player player)
        {
            return new SendGiftResponse { IsSuccess = true, Player = player };
        }

        public static SendGiftResponse Error(string errorMessage)
        {
            return new SendGiftResponse { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}