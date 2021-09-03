using UnityEngine;
using System.Collections.Generic;

public class GameEventTrigger : MonoBehaviour 
{
    public List<EventData> events;

    void Awake()
    {
        foreach (var ev in events)
            ev.Setup();
    }
}

[System.Serializable]
public class EventData
{
    public string name;
    public GameEvent gameEvent;
    public EventCallback response;
    
    public void Setup()
    { gameEvent.Response = response; }
}