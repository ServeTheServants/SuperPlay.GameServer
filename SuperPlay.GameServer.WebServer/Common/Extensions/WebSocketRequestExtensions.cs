using SuperPlay.GameServer.WebServer.Common.Entities;
using System.Collections.Generic;

namespace SuperPlay.GameServer.WebServer.Common.Extensions
{
	public static class WebSocketRequestExtensions
	{
        public static Dictionary<string, object> ToWebSocketRequestLogInformation(this WebSocketRequest webSocketRequest)
        {
            var data = new Dictionary<string, object>
            {
                {"id", webSocketRequest.Id.ToString()},
                {"action", webSocketRequest.Action},
                {"data", webSocketRequest.Data}
            };

            var result = new Dictionary<string, object>
            {
                {"request", data}
            };

            return result;
        }
    }
}