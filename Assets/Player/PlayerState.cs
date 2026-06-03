using UnityEngine;

public abstract class PlayerState
{

    public abstract void Enter(PlayerStateMachine stateMachine);

    public abstract void Exit(PlayerStateMachine stateMachine);

    public abstract PlayerState Update(PlayerStateMachine stateMachine, float deltaTime);
    
}
