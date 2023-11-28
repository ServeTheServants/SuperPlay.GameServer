using System;
using SuperPlay.GameServer.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using SuperPlay.GameServer.Domain.Entities;
using SuperPlay.GameServer.Application.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace SuperPlay.GameServer.Application.SendGift
{
	public sealed class SendGiftRequestHandler : IRequestHandler<SendGiftRequest, SendGiftResponse>
	{
        private readonly ILogger<SendGiftRequestHandler> _logger;

        private readonly IApplicationDbContext _applicationDbContext;

        private readonly IPlayerConnectionContainer _playerConnectionContainer;

        public SendGiftRequestHandler
        (
            ILogger<SendGiftRequestHandler> logger,
            IApplicationDbContext applicationDbContext,
            IPlayerConnectionContainer playerConnectionContainer
        )
        {
            _logger = logger;

            _applicationDbContext = applicationDbContext;

            _playerConnectionContainer = playerConnectionContainer;
        }

        public async Task<SendGiftResponse> HandleAsync(SendGiftRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var playerConnection = _playerConnectionContainer.Get(request.ConnectionId);

            if (playerConnection == null || !playerConnection.IsPlayerOnline())
                return SendGiftResponse.Error("Player is offline");

            var player = await _applicationDbContext.Players.GetPlayerAsync(playerConnection.PlayerId.Value, cancellationToken);

            if (player == null)
                return SendGiftResponse.Error("Player does not exist.");

            var friendPlayer = await _applicationDbContext.Players.GetPlayerAsync(request.FriendPlayerId, cancellationToken);

            if (friendPlayer == null)
                return SendGiftResponse.Error("Friend player does not exist.");

            var sendGiftResponse = request.ResourceType switch
            {
                ResourceType.Coins =>
                     await SendCoinsAsync(player, friendPlayer, request.ResourceValue, cancellationToken),
                ResourceType.Rolls =>
                     await SendRollsAsync(player, friendPlayer, request.ResourceValue, cancellationToken),
                _ =>
                    SendGiftResponse.Error("Invalid resource type.")
            };

            _ = Task.Run(() => SendGiftEventAsync(new GiftEvent(new GiftEventData(player.PlayerId, friendPlayer.PlayerId, request.ResourceType, request.ResourceValue)), cancellationToken), cancellationToken);

            return sendGiftResponse;
        }

        private async Task<SendGiftResponse> SendCoinsAsync(Player player, Player friendPlayer, int coins, CancellationToken cancellationToken)
        {
            if (player.Coins < coins)
                return SendGiftResponse.Error("Not enough coins to send as a gift.");

            player.Coins -= coins;
            friendPlayer.Coins += coins;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return SendGiftResponse.Success(player);
        }

        private async Task<SendGiftResponse> SendRollsAsync(Player player, Player friendPlayer, int rolls, CancellationToken cancellationToken)
        {
            if (player.Rolls < rolls)
                return SendGiftResponse.Error("Not enough rolls to send as a gift.");

            player.Rolls -= rolls;
            friendPlayer.Rolls += rolls;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return SendGiftResponse.Success(player);
        }

        private async Task SendGiftEventAsync(GiftEvent giftEvent, CancellationToken cancellationToken)
        {
            try
            {
                var friendPlayerConnection = _playerConnectionContainer.Get(giftEvent.Data.FriendPlayerId);

                if (friendPlayerConnection == null || !friendPlayerConnection.IsPlayerOnline())
                {
                    _logger.LogInformation(message: $"SendGiftRequestHandler::SendGiftEventAsync. Friend player is offline.", new { FriendPlayerId = friendPlayerConnection .PlayerId});

                    return;
                }

                await friendPlayerConnection.SendEventAsync(giftEvent, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "SendGiftRequestHandler::SendGiftEventAsync. An error occured sending gift.", giftEvent);
            }
        }
    }
}