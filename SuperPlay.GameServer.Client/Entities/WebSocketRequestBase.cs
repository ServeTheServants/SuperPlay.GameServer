using System;

namespace SuperPlay.GameServer.Client.Entities
{
	public abstract class WebSocketRequestBase<TData> where TData : class
	{
        public Guid Id { get; } = Guid.NewGuid();

        public abstract string Action { get; }

        public TData Data { get; }

        protected WebSocketRequestBase(TData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}