using System;

namespace SuperPlay.GameServer.Application.Common.Interfaces
{
	public interface IPlayerConnectionContainer
	{
        IPlayerConnection Get(int playerId);

        IPlayerConnection Get(Guid connectionId);

        void Add(IPlayerConnection playerWebSocketConnection);

        bool Remove(IPlayerConnection playerWebSocketConnection);
    }
}