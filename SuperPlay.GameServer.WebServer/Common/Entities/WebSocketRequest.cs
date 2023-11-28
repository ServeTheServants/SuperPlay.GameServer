using System;
using System.Text.Json;

namespace SuperPlay.GameServer.WebServer.Common.Entities
{
    public record WebSocketRequest
    {
        public Guid Id { get; init; }

        public string Action { get; init; }

        public JsonElement Data { get; init; }
    }
}