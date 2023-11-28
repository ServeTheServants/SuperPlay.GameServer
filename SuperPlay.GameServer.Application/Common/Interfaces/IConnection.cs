using System;
using System.Threading;
using System.Threading.Tasks;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.Common.Interfaces
{
	public interface IConnection
	{
        Guid ConnectionId { get; }

        bool IsConnectionOpen();

        Task SendAsync<TData>(TData data, CancellationToken cancellationToken = default);

        Task SendEventAsync<TData>(EventBase<TData> eventBase, CancellationToken cancellationToken = default) where TData : class;
    }
}