using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.Login
{
    public sealed class LoginResponse
    {
        public bool IsSuccess { get; private set; }

        public string ErrorMessage { get; private set; }

        public Player Player { get; private set; }

        public static LoginResponse Success(Player player)
        {
            return new LoginResponse { IsSuccess = true, Player = player };
        }

        public static LoginResponse Error(string errorMessage)
        {
            return new LoginResponse { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}