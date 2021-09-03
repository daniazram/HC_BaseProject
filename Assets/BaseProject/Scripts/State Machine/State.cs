using System;

public class State
{
    protected StateMachine stateMachine;
    protected Action enterAction;
    protected Action exitAction;
    protected Action<float> executeAction;

    public State(StateMachine stateMachine, Action enterAction, Action<float> executeAction, Action exitAction)
    {
        this.stateMachine = stateMachine;
        this.enterAction = enterAction;
        this.executeAction = executeAction;
        this.exitAction = exitAction;
    }

    public virtual void Enter()
    {
        enterAction?.Invoke();
    }

    public virtual void Execute(float deltaTime)
    {
        executeAction?.Invoke(deltaTime);
    }

    public virtual void Exit()
    {
        exitAction?.Invoke();
    }
}
