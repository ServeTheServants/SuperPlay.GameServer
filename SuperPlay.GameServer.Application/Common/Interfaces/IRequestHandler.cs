using System.Threading;
using System.Threading.Tasks;

namespace SuperPlay.GameServer.Application.Common.Interfaces
{
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}