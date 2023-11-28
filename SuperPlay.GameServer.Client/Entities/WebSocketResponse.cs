using System;

namespace SuperPlay.GameServer.Client.Entities
{
	public record WebSocketResponse<TData>(string Action, TData Data, Guid Id) where TData : class;
}