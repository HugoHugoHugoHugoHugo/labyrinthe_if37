using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaceholderControls : MonoBehaviour
{
    private PlayerInput m_playerInput;
    private Rigidbody m_playerRigidbody;
    private Vector2 m_previousMovement;
    public float MouseSensitivity;
    public float Speed;
    public Camera Camera;
    private AudioSource m_source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerRigidbody = GetComponent<Rigidbody>();
        m_source = GetComponent<AudioSource>();
        m_previousMovement = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMovement = m_playerInput.actions["Movement"].ReadValue<Vector2>();
        if (!currentMovement.Equals(m_previousMovement))
        {
            m_previousMovement=currentMovement;
            Move(currentMovement);
        }
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        Vector3 angularVelocity = new Vector3(0,mouseDelta.x,0);
        Camera.transform.rotation = Quaternion.Euler(Camera.transform.rotation.eulerAngles + angularVelocity*Time.deltaTime*MouseSensitivity);
        Camera.transform.position = transform.position;
        Vector3 playerVelocity = this.gameObject.GetComponent<Rigidbody>().linearVelocity;
        Debug.Log(playerVelocity.magnitude);
        m_source.pitch=SuperLerp(0.7f,1f,0f,.5f,playerVelocity.magnitude);
        m_source.volume=SuperLerp(0f,1f,0f,.5f,playerVelocity.magnitude);
    }

    public void Move(Vector2 movement)
    {
        Vector3 movementVect3 = MovementConvertToVect3(movement);
        Vector3 movX = movementVect3.x * Camera.transform.right;
        Vector3 movZ = movementVect3.z * Camera.transform.forward;
        m_playerRigidbody.AddForce((movX + movZ)*Speed*Time.deltaTime);
        
    }

    public Vector3 MovementConvertToVect3(Vector2 vect2)
    {
        Vector3 vector3 = new Vector3(vect2.x,0,vect2.y);
        return vector3;
    } 
    private float SuperLerp (float from,float to,float from2,float to2,float value) {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        return (to - from) * ((value - from2) / (to2 - from2)) + from;
    }
}
