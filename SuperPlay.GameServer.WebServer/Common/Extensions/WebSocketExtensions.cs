using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SuperPlay.GameServer.WebServer.Common.Extensions
{
	public static class WebSocketExtensions
	{
        public static async Task SendAsync(this WebSocket webSocket, object data, JsonSerializerOptions jsonSerializerOptions, CancellationToken cancellationToken = default)
        {
            if (webSocket == null)
                throw new ArgumentNullException(nameof(webSocket));

            var result = JsonSerializer.Serialize(data, jsonSerializerOptions);

            var buffer = Encoding.UTF8.GetBytes(result);

            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);
        }
    }
}