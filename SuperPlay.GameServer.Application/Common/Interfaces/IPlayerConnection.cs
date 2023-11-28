namespace SuperPlay.GameServer.Application.Common.Interfaces
{
	public interface IPlayerConnection : IConnection
	{
        int? PlayerId { get; }

        void Login(int playerId);

        bool IsPlayerOnline();
    }
}