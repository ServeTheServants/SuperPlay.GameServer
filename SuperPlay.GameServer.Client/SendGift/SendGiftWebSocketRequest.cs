using SuperPlay.GameServer.Client.Constants;
using SuperPlay.GameServer.Client.Entities;

namespace SuperPlay.GameServer.Client.SendGift
{
	public class SendGiftWebSocketRequest : WebSocketRequestBase<SendGiftWebSocketRequestData>
    {
        public SendGiftWebSocketRequest(SendGiftWebSocketRequestData data) : base(data)
        {
        }

        public override string Action =>
            WebSocketActions.SendGift;
    }
}