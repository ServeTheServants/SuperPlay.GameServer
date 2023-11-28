using System;
using SuperPlay.GameServer.WebServer.Common.Entities;
using SuperPlay.GameServer.WebServer.Common.Extensions;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SuperPlay.GameServer.WebServer.Infrastructure.Logger
{
	public sealed class WebSocketLogger
	{
        private readonly ILogger<WebSocketLogger> _logger;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public WebSocketLogger(ILogger<WebSocketLogger> logger, JsonSerializerOptions jsonSerializerOptions)
        {
            _logger = logger;

            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation("{message}", message);
        }

        public void LogInformation(WebSocketRequest webSocketRequest)
        {
            var webSocketRequestLogInformation = webSocketRequest.ToWebSocketRequestLogInformation();

            var webSocketRequestLogInformationJson = JsonSerializer.Serialize(webSocketRequestLogInformation, _jsonSerializerOptions);

            _logger.LogInformation("{webSocketRequestLogInformationJson}", webSocketRequestLogInformationJson);
        }

        public void LogInformation(WebSocketResponse webSocketResponse)
        {
            var webSocketResponseLogInformation = webSocketResponse.ToWebSocketResponseLogInformation();

            var webSocketResponseLogInformationJson = JsonSerializer.Serialize(webSocketResponseLogInformation, _jsonSerializerOptions);

            _logger.LogInformation("{webSocketResponseLogInformationJson}", webSocketResponseLogInformationJson);
        }

        public void LogError(WebSocketError webSocketError, Exception exception)
        {
            var webSocketErrorLogInformation = webSocketError.ToWebSocketErrorLogInformation();

            var webSocketErrorLogInformationJson = JsonSerializer.Serialize(webSocketErrorLogInformation, _jsonSerializerOptions);

            _logger.LogError(exception, "{webSocketErrorLogInformationJson}", webSocketErrorLogInformationJson);
        }
    }
}