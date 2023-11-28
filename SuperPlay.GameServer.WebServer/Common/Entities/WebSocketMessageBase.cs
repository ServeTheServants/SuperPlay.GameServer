namespace SuperPlay.GameServer.WebServer.Common.Entities
{
	public abstract record WebSocketMessageBase
	{
		public abstract string Type { get; }
	}
}