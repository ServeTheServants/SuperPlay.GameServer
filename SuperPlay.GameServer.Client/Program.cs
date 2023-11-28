using System;
using System.Text.Json;
using System.Threading.Tasks;
using SuperPlay.GameServer.Client.Entities;
using SuperPlay.GameServer.Client.Login;
using SuperPlay.GameServer.Client.SendGift;
using SuperPlay.GameServer.Client.UpdateResources;

namespace SuperPlay.GameServer.Client;

class Program
{
    private const string WEB_SERVER_URL = "ws://localhost:8080/ws";

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    static async Task Main()
    {
        Uri uri = new(WEB_SERVER_URL);

        WebSocketClient webSocketClient2 = new(_jsonSerializerOptions, uri);

        await webSocketClient2.SendAsync(new LoginWebSocketRequest(new LoginWebSocketRequestData("2")));

        WebSocketClient webSocketClient1 = new(_jsonSerializerOptions, uri);

        await webSocketClient1.SendAsync(new LoginWebSocketRequest(new LoginWebSocketRequestData("1")));

        await webSocketClient1.SendAsync(new LoginWebSocketRequest(new LoginWebSocketRequestData("1")));

        await webSocketClient1.SendAsync(new UpdateResourcesWebSocketRequest(new UpdateResourcesWebSocketRequestData(ResourceType.Coins, 300)));

        await webSocketClient1.SendAsync(new UpdateResourcesWebSocketRequest(new UpdateResourcesWebSocketRequestData(ResourceType.Rolls, 300)));

        await webSocketClient1.SendAsync(new SendGiftWebSocketRequest(new SendGiftWebSocketRequestData(2, ResourceType.Coins, 10)));

        await webSocketClient1.SendAsync(new SendGiftWebSocketRequest(new SendGiftWebSocketRequestData(2, ResourceType.Coins, 10)));

        await webSocketClient1.SendAsync(new SendGiftWebSocketRequest(new SendGiftWebSocketRequestData(2, ResourceType.Coins, 10)));

        await webSocketClient1.SendAsync(new SendGiftWebSocketRequest(new SendGiftWebSocketRequestData(2, ResourceType.Coins, 10)));

        await webSocketClient1.SendAsync(new SendGiftWebSocketRequest(new SendGiftWebSocketRequestData(2, ResourceType.Rolls, 10)));

        Console.ReadKey();
    }
}