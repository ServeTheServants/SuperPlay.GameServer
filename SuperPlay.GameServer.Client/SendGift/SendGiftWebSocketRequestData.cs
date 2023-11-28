using SuperPlay.GameServer.Client.Entities;

namespace SuperPlay.GameServer.Client.SendGift
{
    public record SendGiftWebSocketRequestData(int FriendPlayerId, ResourceType ResourceType, int ResourceValue);
}