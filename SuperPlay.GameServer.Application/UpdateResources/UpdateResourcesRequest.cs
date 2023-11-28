using System;
using SuperPlay.GameServer.Application.Common.Entities;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Application.UpdateResources
{
    public sealed record UpdateResourcesRequest(Guid ConnectionId, ResourceType ResourceType, int ResourceValue) : ConnectionRequest(ConnectionId), IRequest<UpdateResourcesResponse>;
}