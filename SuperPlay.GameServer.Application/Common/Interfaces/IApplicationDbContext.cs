using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Player> Players { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}