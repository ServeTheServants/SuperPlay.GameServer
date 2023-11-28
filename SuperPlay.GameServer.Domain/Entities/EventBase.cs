using System;

namespace SuperPlay.GameServer.Domain.Entities
{
	public abstract record EventBase<TData> where TData : class
    {
        public TData Data { get; }

        protected EventBase(TData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public abstract string Event { get; }
    }
}