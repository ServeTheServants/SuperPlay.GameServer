using System.Collections.Generic;
using SuperPlay.GameServer.WebServer.Common.Entities;

namespace SuperPlay.GameServer.WebServer.Common.Extensions
{
	public static class WebSocketResponseExtensions
	{
        public static Dictionary<string, object> ToWebSocketResponseLogInformation(this WebSocketResponse webSocketResponse)
        {
            var data = new Dictionary<string, object>
            {
                {"type", webSocketResponse.Type},
                {"id", webSocketResponse.Id},
                {"action", webSocketResponse.Action},
                {"data", webSocketResponse.Data}
            };

            var result = new Dictionary<string, object>
            {
                {"response", data}
            };

            return result;
        }
    }
}