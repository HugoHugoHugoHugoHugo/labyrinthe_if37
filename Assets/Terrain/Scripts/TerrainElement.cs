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
        //UpdateElement();
    }

    /*protected virtual void UpdateElement()
    *{
    *    
    *}*/
    //Moves the audio source to the nearest point of the player within bounds of the object
    public void MoveAudioSourceNearPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 objectPos = transform.position;
        //Gives the position of the player from the object's origin (center of the object)
        Vector3 playerDirection = playerPos - objectPos;
        //Make sure the new position is within bounds of the object
        Vector3 newPos = new Vector3(Mathf.Sign(playerDirection.x)*Mathf.Min(Mathf.Abs(playerDirection.x), m_boundsSize.x*.5f), 
            Mathf.Sign(playerDirection.y)*Mathf.Min(Mathf.Abs(playerDirection.y), m_boundsSize.y*.5f),
            Mathf.Sign(playerDirection.z)*Mathf.Min(Mathf.Abs(playerDirection.z), m_boundsSize.z*.5f));
        //Move the audio source to the new position
        Source.transform.position = transform.position + newPos;
    }

}
