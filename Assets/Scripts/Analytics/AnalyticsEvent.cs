public abstract class AnalyticsEvent
{
    public string eventType;

    public AnalyticsEvent(string eventType)
    {
        this.eventType = eventType;
    }

    public abstract string GetEventData();
}
