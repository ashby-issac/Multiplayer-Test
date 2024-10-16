public class CollectiblePickedEvent : AnalyticsEvent
{
    public string playerId;
    public int collectablesCount = 0;

    public CollectiblePickedEvent(string playerId, string eventName) 
        : base(eventName)
    {
        this.playerId = playerId;
        collectablesCount++;
    }

    public void TrackCollectables()
    {
        collectablesCount++;
    }

    public override string GetEventData()
    {
        return $"PlayerId: {playerId}, Picked Collectables: {collectablesCount}";
    }
}
