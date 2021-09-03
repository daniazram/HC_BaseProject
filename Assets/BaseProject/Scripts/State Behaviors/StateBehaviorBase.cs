using UnityEngine;

public class StateBehaviorBase : MonoBehaviour
{
    public virtual void Enter()
    { }

    public virtual void Execute(float deltaTime)
    { }

    public virtual void Exit()
    { }
}