using System;

public static class EventManager
{
    public static Action<string> OnCollectiblePicked;
    public static Action<int, string, string, string> OnLevelCompleted;

    public static void TriggerCollectiblePicked(string playerId)
    {
        OnCollectiblePicked?.Invoke(playerId);
    }

    public static void TriggerLevelCompleted(int levelId, string playerId, string startTime, string completionTime)
    {
        OnLevelCompleted?.Invoke(levelId, playerId, startTime, completionTime);
    }
}
