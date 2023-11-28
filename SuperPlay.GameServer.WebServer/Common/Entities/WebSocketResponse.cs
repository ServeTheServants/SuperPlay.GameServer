using System;
using SuperPlay.GameServer.WebServer.Common.Constants;

namespace SuperPlay.GameServer.WebServer.Common.Entities
{
    public record WebSocketResponse : WebSocketMessageBase
    {
        public override string Type =>
            WebSocketMessageTypes.Response;

        public Guid Id { get; init; }

        public string Action { get; init; }

        public object Data { get; init; }
    }
}