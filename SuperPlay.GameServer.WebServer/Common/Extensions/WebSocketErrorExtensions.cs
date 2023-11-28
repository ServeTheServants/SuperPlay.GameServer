using System.Collections.Generic;
using SuperPlay.GameServer.WebServer.Common.Entities;

namespace SuperPlay.GameServer.WebServer.Common.Extensions
{
	public static class WebSocketErrorExtensions
	{
        public static Dictionary<string, object> ToWebSocketErrorLogInformation(this WebSocketError webSocketError)
        {
            var data = new Dictionary<string, object>
            {
                {"type", webSocketError.Type},
                {"code", webSocketError.Code},
                {"message", webSocketError.Message},
                {"id", webSocketError.Id}
            };

            var result = new Dictionary<string, object>
            {
                {"response", data}
            };

            return result;
        }
    }
}