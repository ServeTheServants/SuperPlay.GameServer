using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.WebServer.RequestData
{
    public record SendGiftWebSocketRequestData(int FriendPlayerId, ResourceType ResourceType, int ResourceValue);
}