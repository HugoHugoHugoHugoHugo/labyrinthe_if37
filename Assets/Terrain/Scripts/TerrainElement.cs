using NUnit.Framework.Internal;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class TerrainElement : MonoBehaviour
{
    public TerrainElementCaracteristics Caracteristics;

    //Played when element is touched by player
    public AudioClip OnContactSound;
    //Played continuously when touched or not by player
    public AudioClip ContinuousSound;

    public AudioSource Source;
    private Vector3 m_boundsSize;

    void Start()
    {
        //Get the bounds of the object in the form of a box
        m_boundsSize = GetComponent<Renderer>().bounds.size;
    }

    void Update()
    {
        MoveAudioSourceNearPlayer();
    }

    //Moves the audio source to the nearest point of the player within bounds of the object
    void MoveAudioSourceNearPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 objectPos = transform.position;
        //Gives the position of the player from the object's origin (center of the object)
        Vector3 playerDirection = playerPos - objectPos;
        //Transform the vector representing the object's dimensions from local scale to global scale
        Vector3 globalBoundSize = transform.TransformDirection(m_boundsSize);
        //Make sure the new position is within bounds of the object
        Vector3 newPos = new Vector3(Mathf.Sign(playerDirection.x)*Mathf.Min(Mathf.Abs(playerDirection.x), globalBoundSize.x*.5f), 
            Mathf.Sign(playerDirection.y)*Mathf.Min(Mathf.Abs(playerDirection.y), globalBoundSize.y*.5f),
            Mathf.Sign(playerDirection.z)*Mathf.Min(Mathf.Abs(playerDirection.z), globalBoundSize.z*.5f));
        //Move the audio source to the new position
        Source.transform.position = transform.position + newPos;
    }
}
