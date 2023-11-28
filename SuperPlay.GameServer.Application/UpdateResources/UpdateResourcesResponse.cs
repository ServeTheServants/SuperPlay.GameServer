using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.UpdateResources
{
	public sealed record UpdateResourcesResponse
	{
        public bool IsSuccess { get; private set; }

        public string ErrorMessage { get; private set; }

        public Player Player { get; private set; }

        public static UpdateResourcesResponse Success(Player player)
        {
            return new UpdateResourcesResponse { IsSuccess = true, Player = player };
        }

        public static UpdateResourcesResponse Error(string errorMessage)
        {
            return new UpdateResourcesResponse { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}