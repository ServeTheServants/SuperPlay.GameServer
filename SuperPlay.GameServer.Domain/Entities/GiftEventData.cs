namespace SuperPlay.GameServer.Domain.Entities
{
	public record GiftEventData(int PlayerId, int FriendPlayerId, ResourceType ResourceType, int ResourceValue);
}