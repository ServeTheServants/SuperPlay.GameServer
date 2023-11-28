using System;
using SuperPlay.GameServer.Application.Common.Entities;
using SuperPlay.GameServer.Application.Common.Interfaces;

namespace SuperPlay.GameServer.Application.Login
{
    public sealed record LoginRequest(Guid ConnectionId, string DeviceId) : ConnectionRequest(ConnectionId), IRequest<LoginResponse>;
}