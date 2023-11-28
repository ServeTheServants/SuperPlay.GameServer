using SuperPlay.GameServer.Domain.Constants;

namespace SuperPlay.GameServer.Domain.Entities
{
	public record GiftEvent : EventBase<GiftEventData>
	{
        public GiftEvent(GiftEventData data) : base(data) { }

        public override string Event =>
			EventNames.GiftEvent;
    }
}