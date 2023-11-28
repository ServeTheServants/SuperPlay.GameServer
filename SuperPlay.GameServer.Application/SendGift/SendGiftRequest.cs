using System;
using SuperPlay.GameServer.Application.Common.Entities;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.SendGift
{
	public sealed record SendGiftRequest(Guid ConnectionId, int FriendPlayerId, ResourceType ResourceType, int ResourceValue) : ConnectionRequest(ConnectionId), IRequest<SendGiftResponse>;
}