using UnityEngine;
using static Unity.VisualScripting.Member;

public class WallElement : TerrainElement
{
    public float PitchModifier;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 playerVelocity = collision.gameObject.GetComponent<Rigidbody>().linearVelocity;
            Source.PlayOneShot(OnContactSound);
            Source.Play();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 playerVelocity = collision.gameObject.GetComponent<Rigidbody>().linearVelocity;
            Debug.Log(playerVelocity.magnitude);
            Source.pitch=Map(playerVelocity.magnitude,0f,1f,0.7f,1f);
            Source.volume=Map(playerVelocity.magnitude,0f, 1.5f,0f,2f);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Source.Stop();
        }
    }
    public static float SuperLerp (float from,float to,float from2,float to2,float value) {
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

    public static float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
