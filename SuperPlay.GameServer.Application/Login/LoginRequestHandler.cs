using System;
using System.Threading;
using System.Threading.Tasks;
using SuperPlay.GameServer.Application.Common.Extensions;
using SuperPlay.GameServer.Application.Common.Interfaces;

namespace SuperPlay.GameServer.Application.Login
{
    public sealed class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly IPlayerConnectionContainer _playerConnectionContainer;

        public LoginRequestHandler(IApplicationDbContext applicationDbContext, IPlayerConnectionContainer playerConnectionContainer)
        {
            _applicationDbContext = applicationDbContext;

            _playerConnectionContainer = playerConnectionContainer;
        }

        public async Task<LoginResponse> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var player = await _applicationDbContext.Players
                .GetPlayerAsync(request.DeviceId, cancellationToken);

            if (player == null)
                return LoginResponse.Error("Player does not exist.");

            var playerConnection = _playerConnectionContainer.Get(request.ConnectionId);

            if (playerConnection == null || !playerConnection.IsConnectionOpen())
                return LoginResponse.Error("Connection is shut.");

            if (playerConnection.IsPlayerOnline())
                return LoginResponse.Error("Player is already online.");

            playerConnection.Login(player.PlayerId);

            return LoginResponse.Success(player);
        }
    }
}