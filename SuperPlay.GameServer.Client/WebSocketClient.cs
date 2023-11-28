using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SuperPlay.GameServer.Client
{
	public class WebSocketClient
	{
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private readonly Lazy<Task<ClientWebSocket>> _clientWebSocketConnection;

        public WebSocketClient(JsonSerializerOptions jsonSerializerOptions, Uri uri)
        {
            _jsonSerializerOptions = jsonSerializerOptions;

            _clientWebSocketConnection = new Lazy<Task<ClientWebSocket>>(() => InitializeAsync(uri));
        }

        public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        {
            var clientWebSocket = await _clientWebSocketConnection.Value;

            var jsonRequest = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            Console.WriteLine(jsonRequest);

            var requestBuffer = Encoding.UTF8.GetBytes(jsonRequest);
            
            await clientWebSocket.SendAsync(new ArraySegment<byte>(requestBuffer), WebSocketMessageType.Text, true, cancellationToken);
        }

        private static async Task<ClientWebSocket> InitializeAsync(Uri uri)
        {
            var clientWebSocket = new ClientWebSocket();

            await clientWebSocket.ConnectAsync(uri, CancellationToken.None);

            _ = Task.Run(async () =>
            {
                var buffer = new byte[1024 * 4];
                var receiveResult = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!receiveResult.CloseStatus.HasValue)
                {
                    if (receiveResult.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                        Console.WriteLine(message);
                    }

                    receiveResult = await clientWebSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await clientWebSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);
            });

            return clientWebSocket;
        }
    }
}