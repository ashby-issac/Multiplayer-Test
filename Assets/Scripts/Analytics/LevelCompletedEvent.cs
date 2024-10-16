using System;

public class LevelCompletedEvent : AnalyticsEvent
{
    public int levelId;
    public string playerId;
    public string startTime;
    public string completionTime;
    public string totalTime;

    public LevelCompletedEvent(int levelId, string playerId, string startTime, string completionTime, string totalTime, string eventName) 
        : base(eventName)
    {
        this.levelId = levelId;
        this.playerId = playerId;
        this.startTime = startTime;
        this.completionTime = completionTime;
        this.totalTime = totalTime;
    }

    public override string GetEventData()
    {
        TimeSpan totalTime =  DateTime.Parse(completionTime) - DateTime.Parse(startTime);
        return $"Level ID: {levelId}, StartTime: {startTime}, Total Time: {completionTime}s, Total Time: {totalTime}";
    }
}
