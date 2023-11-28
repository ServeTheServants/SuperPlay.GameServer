using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.WebServer.RequestData
{
	public record UpdateResourcesWebSocketRequestData(ResourceType ResourceType, int ResourceValue);
}