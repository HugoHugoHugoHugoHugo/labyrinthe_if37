using UnityEngine;

public class FlagElement : TerrainElement
{
   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Source.PlayOneShot(OnContactSound);
            FlagHit();
            Debug.Log("Hit the win condition !");
        }
    }
    void FlagHit()
    {
        
    }
}
