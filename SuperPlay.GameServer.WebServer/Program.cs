using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.WebServer.Common.Entities;
using SuperPlay.GameServer.WebServer.Infrastructure.Logger;
using SuperPlay.GameServer.WebServer.Infrastructure.Utilities;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SuperPlay.GameServer.WebServer;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog();

        var configuration = ConfigurationHelpers.BuildConfiguration(builder.Environment);

        builder.Services.AddApplicationServices();

        builder.Services.AddInfrastructureServices(configuration);

        builder.Services.AddWebServerServices();

        var app = builder.Build();

        app.UseWebSockets(new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2)
        });

        app.MapGet("/ws", async (
            HttpContext context,
            ILogger<Program> logger,
            JsonSerializerOptions jsonSerializerOptions,
            WebSocketRequestHandler webSocketRequestHandler,
            IPlayerConnectionContainer playerConnectionContainer,
            WebSocketLogger webSocketLogger) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                IPlayerConnection playerConnection = new PlayerWebSocketConnection(Guid.NewGuid(), webSocket, jsonSerializerOptions);

                playerConnectionContainer.Add(playerConnection);

                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!receiveResult.CloseStatus.HasValue)
                {
                    if (receiveResult.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                        webSocketLogger.LogInformation($"Raw message: {message}");

                        WebSocketRequest webSocketRequest = null;

                        try
                        {
                            webSocketRequest = JsonSerializer.Deserialize<WebSocketRequest>(message, jsonSerializerOptions);

                            webSocketLogger.LogInformation(webSocketRequest);

                            var webSocketResponse = await webSocketRequestHandler.HandleAsync(webSocketRequest, playerConnection.ConnectionId);

                            await playerConnection.SendAsync(webSocketResponse);
                        }
                        catch (Exception exception)
                        {
                            var webSocketError = new Common.Entities.WebSocketError
                            {
                                Code = StatusCodes.Status500InternalServerError,
                                Message = exception.Message,
                                Id = webSocketRequest?.Id
                            };

                            webSocketLogger.LogError(webSocketError, exception);

                            await playerConnection.SendAsync(webSocketError);
                        }
                    }

                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);

                playerConnectionContainer.Remove(playerConnection);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        });

        app.Run();
    }
}