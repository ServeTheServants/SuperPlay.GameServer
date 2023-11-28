using System;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.WebServer.Common.Entities
{
    public class PlayerWebSocketConnection : WebSocketConnection, IPlayerConnection
    {
        public int? PlayerId { get; private set; }

        public PlayerWebSocketConnection(Guid connectionId, WebSocket webSocket, JsonSerializerOptions jsonSerializerOptions) : base(connectionId, webSocket, jsonSerializerOptions) { }

        public void Login(int playerId)
        {
            PlayerId = playerId;
        }

        public bool IsPlayerOnline()
        {
            if (!IsConnectionOpen())
                return false;

            return PlayerId != null;
        }

        public override async Task SendEventAsync<TData>(EventBase<TData> eventBase, CancellationToken cancellationToken = default) where TData : class
        {
            if (!IsPlayerOnline())
                return;

            await base.SendEventAsync(eventBase, cancellationToken);
        }
    }
}