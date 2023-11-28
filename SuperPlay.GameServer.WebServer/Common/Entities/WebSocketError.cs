using System;
using SuperPlay.GameServer.WebServer.Common.Constants;

namespace SuperPlay.GameServer.WebServer.Common.Entities
{
	public record WebSocketError : WebSocketMessageBase
    {
		public override string Type =>
			WebSocketMessageTypes.Error;

        public int Code { get; init; }

		public string Message { get; init; }

		public Guid? Id { get; set; }
    }
}