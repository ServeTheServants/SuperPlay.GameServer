using SuperPlay.GameServer.Client.Entities;

namespace SuperPlay.GameServer.Client.UpdateResources
{
	public record UpdateResourcesWebSocketRequestData(ResourceType ResourceType, int ResourceValue);
}