using Microsoft.EntityFrameworkCore;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperPlay.GameServer.Application.UpdateResources
{
    public sealed class UpdateResourcesRequestHandler : IRequestHandler<UpdateResourcesRequest, UpdateResourcesResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly IPlayerConnectionContainer _playerConnectionContainer;

        public UpdateResourcesRequestHandler(IApplicationDbContext applicationDbContext, IPlayerConnectionContainer playerConnectionContainer)
        {
            _applicationDbContext = applicationDbContext;

            _playerConnectionContainer = playerConnectionContainer;
        }

        public async Task<UpdateResourcesResponse> HandleAsync(UpdateResourcesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.ResourceValue < 0)
                return UpdateResourcesResponse.Error("Invalid resource value.");

            var playerConnection = _playerConnectionContainer.Get(request.ConnectionId);

            if (playerConnection == null || !playerConnection.IsPlayerOnline())
                return UpdateResourcesResponse.Error("Player is offline.");

            var player = await _applicationDbContext.Players
                .FirstOrDefaultAsync(player => player.PlayerId == playerConnection.PlayerId.Value, cancellationToken);

            if (player == null)
                return UpdateResourcesResponse.Error("Player not found.");

            return request.ResourceType switch
            {
                ResourceType.Coins =>
                    await UpdateCoinsAsync(player, request.ResourceValue, cancellationToken),
                ResourceType.Rolls =>
                    await UpdateRollsAsync(player, request.ResourceValue, cancellationToken),
                _ =>
                    UpdateResourcesResponse.Error("Invalid resource type.")
            };
        }

        private async Task<UpdateResourcesResponse> UpdateCoinsAsync(Player player, int coins, CancellationToken cancellationToken)
        {
            player.Coins = coins;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return UpdateResourcesResponse.Success(player);
        }

        private async Task<UpdateResourcesResponse> UpdateRollsAsync(Player player, int rolls, CancellationToken cancellationToken)
        {
            player.Rolls = rolls;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return UpdateResourcesResponse.Success(player);
        }
    }
}