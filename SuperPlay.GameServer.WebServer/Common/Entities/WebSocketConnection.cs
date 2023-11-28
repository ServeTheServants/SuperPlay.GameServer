using System;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Domain.Entities;
using SuperPlay.GameServer.WebServer.Common.Extensions;

namespace SuperPlay.GameServer.WebServer.Common.Entities
{
	public class WebSocketConnection : IConnection
	{
        public Guid ConnectionId { get; }

        protected WebSocket WebSocket { get; }

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public WebSocketConnection(Guid connectionId, WebSocket webSocket, JsonSerializerOptions jsonSerializerOptions)
        {
            ConnectionId = connectionId;

            WebSocket = webSocket ?? throw new ArgumentNullException(nameof(webSocket));

            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
        }

        public bool IsConnectionOpen()
        {
            return WebSocket?.State == WebSocketState.Open;
        }

        public virtual async Task SendAsync<TData>(TData data, CancellationToken cancellationToken = default)
        {
            if (!IsConnectionOpen())
                return;

            await WebSocket.SendAsync(data, _jsonSerializerOptions, cancellationToken);
        }

        public virtual async Task SendEventAsync<TData>(EventBase<TData> eventBase, CancellationToken cancellationToken = default) where TData : class
        {
            var webSocketEvent = new WebSocketEvent<TData>
            {
                Event = eventBase.Event,
                Data = eventBase.Data
            };

            await SendAsync(webSocketEvent, cancellationToken);
        }
    }
}