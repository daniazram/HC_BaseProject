using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LevelLayout[] layouts;

    //To test a certain level prefab
    [Header("Testing")]
    [SerializeField]
    private bool testing;
    [SerializeField]
    private int testIndex;

    private bool initialized;
    private StateMachine levelMachine;

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt("index-level", 0);
        set => PlayerPrefs.SetInt("index-level", value);
    }

    public void Init()
    {
        levelMachine = new StateMachine();
        levelMachine.suppressNoStateError = true;
        levelMachine.AddState(GameStateN.MainMenu, ActivateLevel, null, null);

        initialized = true;
    }

    private void ActivateLevel()
    {
        var levelNumber = testing ? testIndex : LevelIndex;
        var layout = Instantiate(layouts[levelNumber % layouts.Length]);
        layout.Init(player);
    }

    public void OnGameStateChanged(object[] data)
    {
        if (!initialized)
            Init();

        var state = (GameStateN)data[0];
        levelMachine.ChangeStateTo(state);
    }
}