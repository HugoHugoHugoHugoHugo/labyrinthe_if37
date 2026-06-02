using UnityEngine;

public class HoleElement : TerrainElement
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HoleHit();
            Debug.Log("Hit a hole");
        }
    }
    void HoleHit()
    {
        
    }
}
