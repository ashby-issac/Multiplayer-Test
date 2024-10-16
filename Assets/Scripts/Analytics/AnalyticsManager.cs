using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AnalyticsManager : MonoBehaviour
{
    private List<AnalyticsEvent> analyticsEvents = new List<AnalyticsEvent>();
    private string filePath;

    public const string CollectiblesPickedEvent = "CollectiblePickedEvent";
    public const string LevelCompletedEvent = "LevelCompletedEvent";

    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, "AnalyticsData.json");

        EventManager.OnCollectiblePicked += OnCollectiblePicked;
        EventManager.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnCollectiblePicked(string playerId)
    {
        var index = analyticsEvents.FindIndex(analyticEvent => analyticEvent.eventType == CollectiblesPickedEvent);
        if (index != -1)
        {
            var collectiblePickedEvent = (CollectiblePickedEvent)analyticsEvents[index];
            collectiblePickedEvent.TrackCollectables();
            return;
        }

        var eventData = new CollectiblePickedEvent(playerId, CollectiblesPickedEvent);
        analyticsEvents.Add(eventData);
    }

    private void OnLevelCompleted(int levelId, string playerId, string startTime, string completionTime)
    {
        var time = DateTime.Parse(completionTime) - DateTime.Parse(startTime);
        var eventData = new LevelCompletedEvent(levelId, playerId, startTime, completionTime, time.ToString(), LevelCompletedEvent);
        analyticsEvents.Add(eventData);
    }

    public void SaveAnalyticsToFile()
    {
        List<string> serializedEvents = new List<string>();

        foreach (var eventData in analyticsEvents)
        {
            serializedEvents.Add(JsonUtility.ToJson(eventData));
        }

        File.WriteAllLines(filePath, serializedEvents);
        Debug.Log("Analytics data saved to: " + filePath);
    }

    private void OnApplicationQuit()
    {
        Debug.Log($":: OnApplicationQuit");
        SaveAnalyticsToFile(); 
    }

    private void OnDestroy()
    {
        Debug.Log($":: OnDestroy");
        SaveAnalyticsToFile();
    }
}
