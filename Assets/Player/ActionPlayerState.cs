using OperatorOverload.Bridge.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionPlayerState : PlayerState
{

    private float forceCoeff = 3f;
    private Vector3 gyroDirection;

    public override void Enter(PlayerStateMachine stateMachine)
    {
        Debug.Log("Entering Action State");
        stateMachine.GetComponent<Rigidbody>().isKinematic = false;
        gyroDirection = Vector3.zero;
    }

    public override void Exit(PlayerStateMachine stateMachine)
    {
    }

    public override PlayerState Update(PlayerStateMachine stateMachine, float deltaTime)
    {
        //RotateTerrain(stateMachine, deltaTime);
        ApplyForceToBall(stateMachine, deltaTime);

        if (Gamepad.current.buttonEast.isPressed)
        {
            return stateMachine.explorationState;
        }
        return null;
    }

    private Vector3 GetControllerAngularVeclocity()
    {
        Vector3 controllerRotation = ((Dualshock4ControllerDevice)(Dualshock4ControllerDevice.current)).gyro.ReadValue();
        controllerRotation.x = -controllerRotation.x;
        controllerRotation.y = -controllerRotation.y;
        return controllerRotation;
    }

    private void ApplyForceToBall(PlayerStateMachine stateMachine, float deltaTime)
    {
        UpdateGyro(deltaTime);

        Rigidbody rb = stateMachine.GetComponent<Rigidbody>();
        Vector3 localForce = new Vector3(-gyroDirection.z, 0, gyroDirection.x);
        Vector3 globalForce = (localForce.x * stateMachine.Camera.transform.right) + (localForce.z * stateMachine.Camera.transform.forward);
        rb.AddForce(globalForce * forceCoeff * deltaTime);
    }
    private void UpdateGyro(float deltaTime)
    {
        gyroDirection += GetControllerAngularVeclocity() * deltaTime;
        if (Gamepad.current.buttonSouth.isPressed)
        {
            gyroDirection = Vector3.zero;
        }
    }

    private void RotateTerrain(PlayerStateMachine stateMachine, float deltaTime)
    {
        Vector3 controllerRotation = GetControllerAngularVeclocity();

        stateMachine.Terrain.transform.rotation = Quaternion.Euler(stateMachine.Terrain.transform.rotation.eulerAngles + controllerRotation * deltaTime);

        if (Gamepad.current.buttonSouth.isPressed)
        {
            stateMachine.Terrain.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
