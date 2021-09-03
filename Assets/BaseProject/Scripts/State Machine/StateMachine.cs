using UnityEngine;

using System;
using System.Collections.Generic;

public class StateMachine
{
    public bool suppressNoStateError;
    protected State currentState;   

    public Dictionary<string, State> States { get; protected set; }

    public StateMachine()
    {
        Init();
    }

    protected virtual void Init()
    {
        States = new Dictionary<string, State>();
    }

    public virtual void AddState<T>(T stateType, Action enterAction, Action<float> executeAction, Action exitAction) where T : System.Enum
    {
        AddState(stateType, new State(this, enterAction, executeAction, exitAction));
    }

    public virtual void AddState<T>(T stateType, State state) where T : System.Enum
    {
        States.Add(stateType.ToString(), state);
    }

    public virtual void ChangeStateTo<T>(T stateType)
    {
        if (!States.ContainsKey(stateType.ToString()))
        {
            if(!suppressNoStateError)
                Debug.LogError($"No state found for {stateType.ToString()}");

            return;
        }

        ChangeStateTo(States[stateType.ToString()]);
    }

    public virtual void ChangeStateTo(State state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    public virtual void Update(float deltaTime)
    {
        currentState?.Execute(deltaTime);
    }
}
