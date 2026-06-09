using UnityEngine;

public class WallElement : TerrainElement
{
    public float PitchModifier;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
            Source.pitch=SuperLerp(0.7f,1f,0f,.5f,playerVelocity.magnitude);
            Source.volume=SuperLerp(0f,1f,0f,.5f,playerVelocity.magnitude);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Source.Stop();
        }
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
