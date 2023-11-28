using SuperPlay.GameServer.Client.Constants;
using SuperPlay.GameServer.Client.Entities;

namespace SuperPlay.GameServer.Client.Login
{
	public class LoginWebSocketRequest : WebSocketRequestBase<LoginWebSocketRequestData>
    {
        public LoginWebSocketRequest(LoginWebSocketRequestData data) : base(data)
        {
        }

        public override string Action =>
            WebSocketActions.Login;
    }
}