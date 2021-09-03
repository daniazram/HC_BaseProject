using UnityEngine;

public enum GameStateN { MainMenu, Gameplay, LevelComplete, LevelFail };

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameStateChangedEvent;
    private StateMachine gameMachine;

    private void Awake()
    {
        Vibration.Init();
        
        gameMachine = new StateMachine();
        gameMachine.AddState(GameStateN.MainMenu, new State(gameMachine, () => gameStateChangedEvent?.Invoke(GameStateN.MainMenu), null, null));
        gameMachine.AddState(GameStateN.Gameplay, new State(gameMachine, () => gameStateChangedEvent?.Invoke(GameStateN.Gameplay), null, null));
        gameMachine.AddState(GameStateN.LevelFail, new State(gameMachine, () => gameStateChangedEvent?.Invoke(GameStateN.LevelFail), null, null));
        gameMachine.AddState(GameStateN.LevelComplete, new State(gameMachine, () => gameStateChangedEvent?.Invoke(GameStateN.LevelComplete), null, null));

        gameMachine.ChangeStateTo(GameStateN.MainMenu);
    }

    private void Update()
    {
        gameMachine.Update(Time.deltaTime);
    }

    //Invoked from play button in UI
    public void OnPlayButtonPressed()
    {
        gameMachine.ChangeStateTo(GameStateN.Gameplay);
    }

    //Invoked by a GameEvent SO (somewhere from LevelLayout.cs) when level is completed or failed
    public void OnLevelFinished(bool cleared)
    {
        gameMachine.ChangeStateTo(cleared ? GameStateN.LevelComplete : GameStateN.LevelFail);
    }
}
