using OperatorOverload.Bridge.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class PlayerStateMachine : MonoBehaviour
{

    public Camera Camera;
    public MarqueurBehavior MarqueurPrefab;

    public float CameraRotationSpeed = 80f;

    public PlayerState explorationState = new ExplorationPlayerState();
    public PlayerState actionState = new ActionPlayerState();

    public Gamepad gamepad;
    [HideInInspector]
    public List<MarqueurBehavior> Marqueurs = new List<MarqueurBehavior>();

    private PlayerState _state = null;

    void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0f;
        SetState(actionState);
        GenerateMarqueurs();
    }

    private void GenerateMarqueurs()
    {
        for (int i = 0; i < 4; i++)
        {
            MarqueurBehavior marqueur = Instantiate(MarqueurPrefab);
            marqueur.Active = false;
            marqueur.Init(i);
            Marqueurs.Add(marqueur);
        }
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

    public void Rumble(float lowFreq, float highFreq, float seconds)
    {
        var command = DualShock4UsbOutputCommand.Create(lowFreq, highFreq, Color.blue);
        Gamepad.current.ExecuteCommand(ref command);
        StartCoroutine(StopRumble(seconds));
    }

    public IEnumerator StopRumble(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        var command = DualShock4UsbOutputCommand.Create(0f, 0f, Color.blue);
        Gamepad.current.ExecuteCommand(ref command);
    }
}
