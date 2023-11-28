using SuperPlay.GameServer.WebServer.Common.Constants;

namespace SuperPlay.GameServer.WebServer.Common.Entities
{
	public record WebSocketEvent<TData> : WebSocketMessageBase where TData : class
	{
        public override string Type =>
            WebSocketMessageTypes.Event;

        public string Event { get; init; }

		public TData Data { get; init; }
    }
}