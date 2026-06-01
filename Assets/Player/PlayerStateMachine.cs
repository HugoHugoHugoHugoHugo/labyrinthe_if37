using OperatorOverload.Bridge.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class PlayerStateMachine : MonoBehaviour
{

    public GameObject Terrain;
    public Camera Camera;

    public float CameraRotationSpeed = 10f;

    public PlayerState explorationState = new ExplorationPlayerState();
    public PlayerState actionState = new ActionPlayerState();

    public Gamepad gamepad;

    private PlayerState _state = null;

    void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0f;
        SetState(actionState);
    }

    void Update()
    {
        HandleCamera();

        PlayerState newState = _state.Update(this, Time.deltaTime);
        if (newState != null)
        {
            SetState(newState);
        }
    }

    private void SetState(PlayerState newState)
    {
        if (_state != null)
            _state.Exit(this);
        _state = newState;
        _state.Enter(this);
    }

    private void HandleCamera()
    {
        Camera.transform.position = transform.position - Camera.transform.forward * 4f;

        Vector3 gamepadRotation = new Vector3(0, Gamepad.current.rightStick.ReadValue().x, 0);
        Camera.transform.rotation = Quaternion.Euler(Camera.transform.rotation.eulerAngles + gamepadRotation * CameraRotationSpeed * Time.deltaTime);

        transform.GetChild(0).transform.rotation = Camera.transform.rotation; // Pour le son
    }
}
