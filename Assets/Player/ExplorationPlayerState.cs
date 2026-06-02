using UnityEngine;
using UnityEngine.InputSystem;

public class ExplorationPlayerState : PlayerState
{

    private float speed = 3f;
    private Vector3 initalPos;

    private InputAction marqueurAction;

    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Entering Exploration State");
        //stateMachine.Terrain.transform.rotation = Quaternion.Euler(Vector3.zero);
        stateMachine.GetComponent<Rigidbody>().isKinematic = true;
        initalPos = stateMachine.transform.position;
        marqueurAction = InputSystem.actions.FindAction("MarqueurAction");
        stateMachine.Rumble(0.0f, 0.5f, 0.5f);
    }

    public override void Exit(PlayerStateMachine stateMachine)
    {
        stateMachine.transform.position = initalPos;
    }

    public override PlayerState Update(PlayerStateMachine stateMachine, float deltaTime)
    {
        MovePlayer(stateMachine, deltaTime);
        PlaceMarqueur(stateMachine);

        if (Gamepad.current.rightShoulder.isPressed)
        {
            return stateMachine.actionState;
        }
        return null;
    }

    public void MovePlayer(PlayerStateMachine stateMachine, float deltaTime)
    {
        Vector2 direction = Gamepad.current.leftStick.ReadValue();
        Vector3 globalDelta = (direction.x * stateMachine.Camera.transform.right) + (direction.y * stateMachine.Camera.transform.forward);
        Vector3 newPos = stateMachine.transform.position + globalDelta * speed * deltaTime;
        stateMachine.transform.position = newPos;
    }

    public void PlaceMarqueur(PlayerStateMachine stateMachine)
    {
        int marqueurSelected = MarqueurBehavior.Vec2ToMarqueur(marqueurAction.ReadValue<Vector2>());
        if (marqueurSelected != -1)
        {
            MarqueurBehavior marqueur = stateMachine.Marqueurs[marqueurSelected];
            marqueur.transform.position = stateMachine.transform.position;
            marqueur.Active = true;
        }
    }
}
