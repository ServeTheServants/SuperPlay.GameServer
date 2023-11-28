using SuperPlay.GameServer.Client.Constants;
using SuperPlay.GameServer.Client.Entities;

namespace SuperPlay.GameServer.Client.UpdateResources
{
	public class UpdateResourcesWebSocketRequest : WebSocketRequestBase<UpdateResourcesWebSocketRequestData>
    {
        public UpdateResourcesWebSocketRequest(UpdateResourcesWebSocketRequestData data) : base(data)
        {
        }

        public override string Action =>
            WebSocketActions.UpdateResources;
    }
}