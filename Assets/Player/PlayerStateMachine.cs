using OperatorOverload.Bridge.Serialization;
using System;
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

    [HideInInspector]
    public delegate void PlayerCollisionEvent(Collision collision);
    [HideInInspector]
    public PlayerCollisionEvent CollisionEvent;

    public PlayerState State = null;

    private Vector3 direction = new Vector3(0, 0, 0);

    void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0f;
        SetState(actionState);
        GenerateMarqueurs();
        direction = transform.forward;
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

        PlayerState newState = State.Update(this, Time.deltaTime);
        if (newState != null)
        {
            SetState(newState);
        }

        SetRollVolume();
    }

    private void SetState(PlayerState newState)
    {
        if (State != null)
            State.Exit(this);
        State = newState;
        State.Enter(this);
    }

    private void HandleCamera()
    {
        Vector3 gamepadRotation = new Vector3(0, -Gamepad.current.rightStick.ReadValue().x, 0) * CameraRotationSpeed * Time.deltaTime;
        direction = Quaternion.Euler(gamepadRotation) * direction;

        Camera.transform.position = transform.position - direction * 4f + new Vector3(0, 2.5f, 0);

        Camera.transform.LookAt(transform);
        transform.GetChild(0).LookAt(transform.GetChild(0).position + direction); // Pour le son
        Debug.DrawLine(transform.GetChild(0).transform.position, transform.GetChild(0).transform.position + transform.GetChild(0).transform.forward * 2f, Color.red);
    }

    private void SetRollVolume()
    {
        GetComponent<AudioSource>().pitch = WallElement.Map(GetComponent<Rigidbody>().linearVelocity.magnitude, 0f, 1f, .7f, 1f);
        GetComponent<AudioSource>().volume = WallElement.Map(GetComponent<Rigidbody>().linearVelocity.magnitude, 0f, 1f, 0f, .1f);
        //Mathf.Clamp(GetComponent<AudioSource>().volume, 0f, 1f);
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

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEvent.Invoke(collision);

        if (collision.gameObject.tag.Equals("Wall"))
        {
            Rumble(0.2f, 0.5f, .4f);
        }
    }
}
