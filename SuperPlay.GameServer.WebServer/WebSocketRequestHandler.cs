using SuperPlay.GameServer.Application.Login;
using SuperPlay.GameServer.Application.SendGift;
using SuperPlay.GameServer.Application.UpdateResources;
using SuperPlay.GameServer.WebServer.Common.Constants;
using SuperPlay.GameServer.WebServer.Common.Entities;
using SuperPlay.GameServer.WebServer.RequestData;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SuperPlay.GameServer.WebServer
{
    public class WebSocketRequestHandler
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private readonly LoginRequestHandler _loginRequestHandler;

        private readonly UpdateResourcesRequestHandler _updateResourcesRequestHandler;

        private readonly SendGiftRequestHandler _sendGiftRequestHandler;

        public WebSocketRequestHandler(
            JsonSerializerOptions jsonSerializerOptions,
            LoginRequestHandler loginRequestHandler,
            UpdateResourcesRequestHandler updateResourcesRequestHandler,
            SendGiftRequestHandler sendGiftRequestHandler)
        {
            _jsonSerializerOptions = jsonSerializerOptions;

            _loginRequestHandler = loginRequestHandler;

            _updateResourcesRequestHandler = updateResourcesRequestHandler;

            _sendGiftRequestHandler = sendGiftRequestHandler;
        }

        public async Task<WebSocketResponse> HandleAsync(WebSocketRequest webSocketRequest, Guid connectionId, CancellationToken cancellationToken = default) =>
            webSocketRequest?.Action switch
            {
                WebSocketActions.Login => await HandleLoginActionAsync(webSocketRequest, connectionId, cancellationToken),
                WebSocketActions.UpdateResources => await HandleUpdateResourcesActionAsync(webSocketRequest, connectionId, cancellationToken),
                WebSocketActions.SendGift => await HandleSendGiftActionAsync(webSocketRequest, connectionId, cancellationToken),
                _ => throw new NotSupportedException($"Unsupported action: {webSocketRequest?.Action}")
            };

        private async Task<WebSocketResponse> HandleLoginActionAsync(WebSocketRequest webSocketRequest, Guid connectionId, CancellationToken cancellationToken)
        {
            var loginWebSocketRequestData = webSocketRequest.Data.Deserialize<LoginWebSocketRequestData>(_jsonSerializerOptions);

            var loginRequest = new LoginRequest(connectionId, loginWebSocketRequestData.DeviceId);

            var loginResponse = await _loginRequestHandler.HandleAsync(loginRequest, cancellationToken);

            return new WebSocketResponse
            {
                Id = webSocketRequest.Id,
                Action = webSocketRequest.Action,
                Data = loginResponse
            };
        }

        private async Task<WebSocketResponse> HandleUpdateResourcesActionAsync(WebSocketRequest webSocketRequest, Guid connectionId, CancellationToken cancellationToken)
        {
            var updateResourcesWebSocketRequestData = webSocketRequest.Data.Deserialize<UpdateResourcesWebSocketRequestData>(_jsonSerializerOptions);

            var updateResourcesRequest = new UpdateResourcesRequest(connectionId, updateResourcesWebSocketRequestData.ResourceType, updateResourcesWebSocketRequestData.ResourceValue);

            var updateResourcesResponse = await _updateResourcesRequestHandler.HandleAsync(updateResourcesRequest, cancellationToken);

            return new WebSocketResponse
            {
                Id = webSocketRequest.Id,
                Action = webSocketRequest.Action,
                Data = updateResourcesResponse
            };
        }

        private async Task<WebSocketResponse> HandleSendGiftActionAsync(WebSocketRequest webSocketRequest, Guid connectionId, CancellationToken cancellationToken)
        {
            var sendGiftWebSocketRequestData = webSocketRequest.Data.Deserialize<SendGiftWebSocketRequestData>(_jsonSerializerOptions);

            var sendGiftRequest = new SendGiftRequest(connectionId, sendGiftWebSocketRequestData.FriendPlayerId, sendGiftWebSocketRequestData.ResourceType, sendGiftWebSocketRequestData.ResourceValue);

            var sendGiftResponse = await _sendGiftRequestHandler.HandleAsync(sendGiftRequest, cancellationToken);

            return new WebSocketResponse
            {
                Id = webSocketRequest.Id,
                Action = webSocketRequest.Action,
                Data = sendGiftResponse
            };
        }
    }
}