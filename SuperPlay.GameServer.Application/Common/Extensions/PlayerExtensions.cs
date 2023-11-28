using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.Common.Extensions
{
	public static class PlayerExtensions
	{
        public static async Task<Player> GetPlayerAsync(this IQueryable<Player> players, int playerId, CancellationToken cancellationToken = default)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            return await players.FirstOrDefaultAsync(player => player.PlayerId == playerId, cancellationToken);
        }

        public static async Task<Player> GetPlayerAsync(this IQueryable<Player> players, string deviceId, CancellationToken cancellationToken = default)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            return await players.FirstOrDefaultAsync(player => player.DeviceId == deviceId, cancellationToken);
        }
    }
}