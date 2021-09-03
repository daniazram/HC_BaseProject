using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameEvent : ScriptableObject 
{
    public GameEventType eventType;
    private EventCallback response;

    public EventCallback Response
    {
        set{ response = value;}
    }
    
    public void Invoke(params object[] data)
    {
        response.Invoke(data);
    }
}

[System.Serializable]
public class EventCallback : UnityEvent<object[]>
{ }

public enum GameEventType { GameState, LevelState, Gameplay, VictoryAnimation, Input }
