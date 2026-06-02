using UnityEngine;

public class WallElement : TerrainElement
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Source.PlayOneShot(OnContactSound);
            Source.Play();
            Debug.Log("Hit a wall");
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Source.Stop();
            Debug.Log("Left a wall");
        }
    }
}
